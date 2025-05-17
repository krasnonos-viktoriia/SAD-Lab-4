using Xunit;
using Moq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using BLL.Models;
using BLL.Services;
using DAL.UnitOfWork;
using Domain.Entities;
using DAL.Repositories;
using BLL.MappingProfiles;
using Microsoft.EntityFrameworkCore.Query;

namespace Tests
{
    public class VisitServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepository<Visit>> _visitRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly VisitService _service;

        public VisitServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _visitRepoMock = new Mock<IRepository<Visit>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.SetupGet(u => u.Visits).Returns(_visitRepoMock.Object);

            _service = new VisitService(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedVisits()
        {
            var visits = new List<Visit> { new Visit { Id = 1, UserId = 5 } };
            _visitRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(visits);

            var result = await _service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(5, result.First().UserId);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedVisit()
        {
            var visit = new Visit { Id = 2, UserId = 3 };
            _visitRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(visit);

            var result = await _service.GetByIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal(3, result!.UserId);
        }

        [Fact]
        public async Task AddAsync_CallsAddAndSave()
        {
            var model = new VisitModel { Id = 1, UserId = 10 };

            await _service.AddAsync(model);

            _visitRepoMock.Verify(r => r.AddAsync(It.IsAny<Visit>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenExists_CallsRemoveAndSave()
        {
            var visit = new Visit { Id = 1 };
            _visitRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(visit);

            await _service.DeleteAsync(1);

            _visitRepoMock.Verify(r => r.Remove(visit), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByUserIdAsync_ReturnsUserVisits()
        {
            var data = new List<Visit> { new Visit { Id = 1, UserId = 99 } }.AsQueryable();

            var mockSet = new Mock<DbSet<Visit>>();
            mockSet.As<IAsyncEnumerable<Visit>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Visit>(data.GetEnumerator()));
            mockSet.As<IQueryable<Visit>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Visit>(data.Provider));
            mockSet.As<IQueryable<Visit>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Visit>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Visit>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _unitOfWorkMock.Setup(u => u.Set<Visit>()).Returns(mockSet.Object);

            var result = await _service.GetByUserIdAsync(99);

            Assert.Single(result);
            Assert.Equal(99, result.First().UserId);
        }

        [Fact]
        public async Task GetByUserIdWithIncludesAsync_ReturnsVisitsWithPlaces()
        {
            var place = new Place { Id = 1, Name = "Place A" };
            var visits = new List<Visit> { new Visit { Id = 1, UserId = 77, Place = place } }.AsQueryable();

            var mockSet = new Mock<DbSet<Visit>>();
            mockSet.As<IAsyncEnumerable<Visit>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Visit>(visits.GetEnumerator()));
            mockSet.As<IQueryable<Visit>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Visit>(visits.Provider));
            mockSet.As<IQueryable<Visit>>().Setup(m => m.Expression).Returns(visits.Expression);
            mockSet.As<IQueryable<Visit>>().Setup(m => m.ElementType).Returns(visits.ElementType);
            mockSet.As<IQueryable<Visit>>().Setup(m => m.GetEnumerator()).Returns(visits.GetEnumerator());

            _unitOfWorkMock.Setup(u => u.Set<Visit>()).Returns(mockSet.Object);

            var result = await _service.GetByUserIdWithIncludesAsync(77);

            Assert.Single(result);
            Assert.Equal("Place A", result.First().Place.Name);
        }

        // Async Enumerator
        internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;
            public TestAsyncEnumerator(IEnumerator<T> inner) => _inner = inner;
            public T Current => _inner.Current;
            public ValueTask DisposeAsync() => new ValueTask();
            public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());
        }

        internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;
            public TestAsyncQueryProvider(IQueryProvider inner) => _inner = inner;

            public IQueryable CreateQuery(Expression expression) => new TestAsyncEnumerable<TEntity>(expression);
            public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestAsyncEnumerable<TElement>(expression);
            public object Execute(Expression expression) => _inner.Execute(expression);
            public TResult Execute<TResult>(Expression expression) => _inner.Execute<TResult>(expression);
            public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default) =>
                Task.FromResult(Execute<TResult>(expression)).Result;
        }

        internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
        {
            public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
            public TestAsyncEnumerable(Expression expression) : base(expression) { }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) =>
                new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());

            IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
        }
    }
}