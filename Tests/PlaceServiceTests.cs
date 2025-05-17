using Xunit;
using Moq;
using AutoMapper;
using BLL.Services;
using BLL.Models;
using DAL.UnitOfWork;
using DAL.Repositories;
using Domain.Entities;
using BLL.MappingProfiles;

namespace Tests
{
    public class PlaceServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepository<Place>> _placeRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly PlaceService _service;

        public PlaceServiceTests()
        {
            // Реальний AutoMapper із профілем
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            // Моки для репозиторію та юніт-оф-ворк
            _placeRepoMock = new Mock<IRepository<Place>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(u => u.Places).Returns(_placeRepoMock.Object);

            // Сервіс з моками
            _service = new PlaceService(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedPlaces()
        {
            // Arrange
            var places = new List<Place>
            {
                new Place { Id = 1, Name = "Kyiv", Address = "Main St", Description = "Capital" },
                new Place { Id = 2, Name = "Lviv", Address = "Square", Description = "Historic" }
            };
            _placeRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(places);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Name == "Kyiv");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedPlace()
        {
            var place = new Place { Id = 1, Name = "Kyiv", Address = "Main St", Description = "Capital" };
            _placeRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(place);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Kyiv", result.Name);
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAndSave()
        {
            var model = new PlaceModel { Id = 1, Name = "Kyiv", Address = "Main", Description = "Info" };

            await _service.AddAsync(model);

            _placeRepoMock.Verify(r => r.AddAsync(It.IsAny<Place>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryAndSave()
        {
            var model = new PlaceModel { Id = 2, Name = "Lviv", Address = "Center", Description = "History" };

            await _service.UpdateAsync(model);

            _placeRepoMock.Verify(r => r.Update(It.IsAny<Place>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenPlaceExists_RemovesAndSaves()
        {
            var place = new Place { Id = 3, Name = "Odessa" };
            _placeRepoMock.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(place);

            await _service.DeleteAsync(3);

            _placeRepoMock.Verify(r => r.Remove(It.Is<Place>(p => p.Id == 3)), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenPlaceNotExists_DoesNothing()
        {
            _placeRepoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Place)null);

            await _service.DeleteAsync(99);

            _placeRepoMock.Verify(r => r.Remove(It.IsAny<Place>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
        }
    }
}
