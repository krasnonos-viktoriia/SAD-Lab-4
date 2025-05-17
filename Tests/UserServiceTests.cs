using Xunit;
using AutoMapper;
using BLL.Models;
using BLL.Services;
using DAL.UnitOfWork;
using Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.MappingProfiles;
using DAL.Repositories;

namespace Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRepository<User>> _userRepoMock;
        private readonly IMapper _mapper;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepoMock = new Mock<IRepository<User>>();
            _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepoMock.Object);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _service = new UserService(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "Alice", Email = "a@example.com" },
                new User { Id = 2, Name = "Bob", Email = "b@example.com" }
            };

            _userRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.Name == "Alice");
            Assert.Contains(result, u => u.Email == "b@example.com");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedUser()
        {
            // Arrange
            var user = new User { Id = 5, Name = "TestUser", Email = "test@example.com" };
            _userRepoMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(user);

            // Act
            var result = await _service.GetByIdAsync(5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestUser", result!.Name);
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAndSave()
        {
            // Arrange
            var model = new UserModel { Name = "New", Email = "new@example.com" };

            // Act
            await _service.AddAsync(model);

            // Assert
            _userRepoMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryAndSave()
        {
            // Arrange
            var model = new UserModel { Id = 1, Name = "Updated", Email = "u@example.com" };

            // Act
            await _service.UpdateAsync(model);

            // Assert
            _userRepoMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_UserExists_DeletesAndSaves()
        {
            // Arrange
            var user = new User { Id = 1, Name = "ToDelete" };
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            // Act
            await _service.DeleteAsync(1);

            // Assert
            _userRepoMock.Verify(r => r.Remove(user), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_UserNotFound_DoesNothing()
        {
            // Arrange
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((User)null!);

            // Act
            await _service.DeleteAsync(1);

            // Assert
            _userRepoMock.Verify(r => r.Remove(It.IsAny<User>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
        }
    }
}
