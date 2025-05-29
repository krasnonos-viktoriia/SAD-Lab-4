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
    public class MediaFileServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepository<MediaFile>> _mediaRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly MediaFileService _service;

        public MediaFileServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            _mediaRepoMock = new Mock<IRepository<MediaFile>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(u => u.MediaFiles).Returns(_mediaRepoMock.Object);

            _service = new MediaFileService(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedList()
        {
            // Arrange
            var entities = new List<MediaFile>
            {
                new MediaFile { Id = 1, FilePath = "img1.jpg", PlaceId = 1 },
                new MediaFile { Id = 2, FilePath = "img2.jpg", PlaceId = 2 }
            };
            _mediaRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, f => f.FilePath == "img1.jpg");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedItem()
        {
            var entity = new MediaFile { Id = 1, FilePath = "img.jpg", PlaceId = 1 };
            _mediaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("img.jpg", result.FilePath);
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAddAndSave()
        {
            var model = new MediaFileModel { Id = 1, FilePath = "new.jpg", PlaceId = 1 };

            await _service.AddAsync(model);

            _mediaRepoMock.Verify(r => r.AddAsync(It.IsAny<MediaFile>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeletesItemIfExists()
        {
            var entity = new MediaFile { Id = 1 };
            _mediaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

            await _service.DeleteAsync(1);

            _mediaRepoMock.Verify(r => r.Remove(It.Is<MediaFile>(f => f.Id == 1)), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByPlaceIdAsync_ReturnsMappedFiles()
        {
            var files = new List<MediaFile>
            {
                new MediaFile { Id = 1, PlaceId = 10, FilePath = "file1.jpg" },
                new MediaFile { Id = 2, PlaceId = 10, FilePath = "file2.jpg" }
            };
            _mediaRepoMock.Setup(r => r.FindAsync(f => f.PlaceId == 10))
                          .ReturnsAsync(files);

            var result = await _service.GetByPlaceIdAsync(10);

            Assert.Equal(2, result.Count());
            Assert.All(result, f => Assert.Equal(10, f.PlaceId));
        }
    }
}