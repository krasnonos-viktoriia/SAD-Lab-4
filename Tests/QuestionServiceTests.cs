using Xunit;
using Moq;
using AutoMapper;
using BLL.Services;
using BLL.Models;
using DAL.UnitOfWork;
using DAL.Repositories;
using Domain.Entities;
using BLL.MappingProfiles;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class QuestionServiceTests
    {
        private readonly Mock<IRepository<Question>> _questionRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly QuestionService _service;

        public QuestionServiceTests()
        {
            _questionRepositoryMock = new Mock<IRepository<Question>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(u => u.Questions).Returns(_questionRepositoryMock.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            _service = new QuestionService(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedQuestions()
        {
            // Arrange
            var questions = new List<Question>
            {
                new Question { Id = 1, Text = "Q1" },
                new Question { Id = 2, Text = "Q2" }
            };

            _questionRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(questions);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                q => Assert.Equal("Q1", q.Text),
                q => Assert.Equal("Q2", q.Text));
        }

        [Fact]
        public async Task GetAllWithIncludesAsync_ReturnsMappedQuestionsWithRelations()
        {
            // Arrange: створення зв’язків
            var user = new User { Id = 1, Name = "User1" };
            var place = new Place { Id = 2, Name = "Place1" };
            var questions = new List<Question>
            {
                new Question { Id = 1, Text = "Q1", User = user, Place = place },
            new Question { Id = 2, Text = "Q2", User = user, Place = place }
            };

            // Мок DbSet<Question>
            var questionDbSetMock = new Mock<DbSet<Question>>();

            // Налаштування IQueryable
            var queryable = questions.AsQueryable();
            questionDbSetMock.As<IQueryable<Question>>().Setup(m => m.Provider).Returns(queryable.Provider);
            questionDbSetMock.As<IQueryable<Question>>().Setup(m => m.Expression).Returns(queryable.Expression);
            questionDbSetMock.As<IQueryable<Question>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            questionDbSetMock.As<IQueryable<Question>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            // Налаштування IAsyncEnumerable
            questionDbSetMock.As<IAsyncEnumerable<Question>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Question>(questions.GetEnumerator()));

            // Мок UnitOfWork
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Set<Question>()).Returns(questionDbSetMock.Object);

            // Реальний мапер
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            var service = new QuestionService(unitOfWorkMock.Object, mapper);

            // Act
            var result = await service.GetAllWithIncludesAsync();

            // Assert
            Assert.NotNull(result);
            var list = result.ToList();
            Assert.Equal(2, list.Count);
            Assert.Equal("Q1", list[0].Text);
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

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedQuestion()
        {
            // Arrange
            var question = new Question { Id = 5, Text = "Test?" };

            _questionRepositoryMock
                .Setup(repo => repo.GetByIdAsync(5))
                .ReturnsAsync(question);

            // Act
            var result = await _service.GetByIdAsync(5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test?", result!.Text);
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAndSave()
        {
            // Arrange
            var model = new QuestionModel { Id = 1, Text = "New Question", PlaceId = 2, UserId = 3 };

            // Act
            await _service.AddAsync(model);

            // Assert
            _questionRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Question>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsUpdateAndSave()
        {
            // Arrange
            var model = new QuestionModel { Id = 2, Text = "Updated", PlaceId = 1, UserId = 1 };

            // Act
            await _service.UpdateAsync(model);

            // Assert
            _questionRepositoryMock.Verify(r => r.Update(It.IsAny<Question>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenExists_CallsRemoveAndSave()
        {
            // Arrange
            var question = new Question { Id = 3, Text = "To delete" };

            _questionRepositoryMock
                .Setup(r => r.GetByIdAsync(3))
                .ReturnsAsync(question);

            // Act
            await _service.DeleteAsync(3);

            // Assert
            _questionRepositoryMock.Verify(r => r.Remove(question), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task AnswerQuestionAsync_WhenExists_UpdatesAndSaves()
        {
            // Arrange
            var question = new Question { Id = 4, Text = "?", Answer = null };

            _questionRepositoryMock
                .Setup(r => r.GetByIdAsync(4))
                .ReturnsAsync(question);

            // Act
            await _service.AnswerQuestionAsync(4, "Answer");

            // Assert
            Assert.Equal("Answer", question.Answer);
            _questionRepositoryMock.Verify(r => r.Update(question), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}
