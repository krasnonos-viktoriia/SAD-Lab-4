using Xunit;
using Moq;
using BLL.Models;
using BLL.Services;
using DAL.UnitOfWork;
using Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DAL.Repositories;
using BLL.MappingProfiles;

namespace Tests
{
    public class ReviewServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepository<Review>> _reviewRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ReviewService _service;

        public ReviewServiceTests()
        {
            // AutoMapper профіль
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Review, ReviewModel>().ReverseMap();
                cfg.CreateMap<Place, PlaceModel>().ReverseMap();
                cfg.CreateMap<User, UserModel>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _reviewRepoMock = new Mock<IRepository<Review>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.SetupGet(u => u.Reviews).Returns(_reviewRepoMock.Object);

            _service = new ReviewService(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedList()
        {
            var data = new List<Review>
            {
                new Review { Id = 1, Comment = "Excellent!" }
            };

            _reviewRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(data);

            var result = await _service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal("Excellent!", result.First().Comment);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedReview()
        {
            var review = new Review { Id = 1, Comment = "Nice place" };

            _reviewRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(review);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Nice place", result!.Comment);
        }

        [Fact]
        public async Task AddAsync_CallsAddAndSave()
        {
            var model = new ReviewModel { Id = 1, Comment = "Add me" };

            await _service.AddAsync(model);

            _reviewRepoMock.Verify(r => r.AddAsync(It.IsAny<Review>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsUpdateAndSave()
        {
            var model = new ReviewModel { Id = 1, Comment = "Update me" };

            await _service.UpdateAsync(model);

            _reviewRepoMock.Verify(r => r.Update(It.IsAny<Review>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenExists_CallsRemoveAndSave()
        {
            var review = new Review { Id = 1 };

            _reviewRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(review);

            await _service.DeleteAsync(1);

            _reviewRepoMock.Verify(r => r.Remove(review), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByPlaceIdAsync_ReturnsCorrectItems()
        {
            var data = new List<Review>
            {
                new Review { Id = 1, PlaceId = 99, Comment = "Place 99 review" }
            };

            _reviewRepoMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Review, bool>>>())).ReturnsAsync(data);

            var result = await _service.GetByPlaceIdAsync(99);

            Assert.Single(result);
            Assert.Equal("Place 99 review", result.First().Comment);
        }


        [Fact]
        public async Task GetAllWithIncludesAsync_ReturnsMappedReviewsWithRelations()
        {
            // Arrange: створення зв’язків
            var user = new User { Id = 1, Name = "User1" };
            var place = new Place { Id = 2, Name = "Place1" };
            var reviews = new List<Review>
            {
                new Review { Id = 1, Comment = "Review1", User = user, Place = place },
                new Review { Id = 2, Comment = "Review2", User = user, Place = place }
            };

            // Мок DbSet<Review>
            var reviewDbSetMock = new Mock<DbSet<Review>>();

            // Налаштування IQueryable
            var queryable = reviews.AsQueryable();
            reviewDbSetMock.As<IQueryable<Review>>().Setup(m => m.Provider).Returns(queryable.Provider);
            reviewDbSetMock.As<IQueryable<Review>>().Setup(m => m.Expression).Returns(queryable.Expression);
            reviewDbSetMock.As<IQueryable<Review>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            reviewDbSetMock.As<IQueryable<Review>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            // Налаштування IAsyncEnumerable
            reviewDbSetMock.As<IAsyncEnumerable<Review>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Review>(reviews.GetEnumerator()));

            // Мок IUnitOfWork
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Set<Review>()).Returns(reviewDbSetMock.Object);

            // Реальний AutoMapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            var service = new ReviewService(unitOfWorkMock.Object, mapper);

            // Act
            var result = await service.GetAllWithIncludesAsync();

            // Assert
            Assert.NotNull(result);
            var list = result.ToList();
            Assert.Equal(2, list.Count);
            Assert.Equal("Review1", list[0].Comment);
            Assert.Equal("User1", list[0].User.Name);
            Assert.Equal("Place1", list[0].Place.Name);
        }

        // Async Enumerator для підтримки IAsyncEnumerable
        internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public ValueTask DisposeAsync() => new ValueTask();
            public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());
            public T Current => _inner.Current;
        }
    }
}