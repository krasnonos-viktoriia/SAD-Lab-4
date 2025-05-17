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




//using Xunit;
//using Moq;
//using AutoMapper;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Linq;
//using System;
//using BLL.Services;
//using BLL.Models;
//using DAL.UnitOfWork;
//using DAL.Repositories;
//using Domain.Entities;
//using BLL.MappingProfiles;

//namespace BLL.Tests.Services
//{
//    public class MediaFileServiceTests
//    {
//        private readonly IMapper _mapper;

//        public MediaFileServiceTests()
//        {
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile<MappingProfile>(); // Реальний профіль мапінгу
//            });
//            _mapper = config.CreateMapper();
//        }

//        [Fact]
//        public async Task GetAllAsync_ReturnsMappedMediaFiles()
//        {
//            // Arrange
//            var fakeMediaFiles = new List<MediaFile>
//            {
//                new MediaFile { Id = 1, FilePath = "file1.jpg", PlaceId = 1, FileType = Domain.Entities.Enums.FileType.Photo },
//                new MediaFile { Id = 2, FilePath = "file2.mp4", PlaceId = 2, FileType = Domain.Entities.Enums.FileType.Video }
//            };

//            var mediaRepoMock = new Mock<IRepository<MediaFile>>();
//            mediaRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(fakeMediaFiles);

//            var unitOfWorkMock = new Mock<IUnitOfWork>();
//            unitOfWorkMock.Setup(u => u.MediaFiles).Returns(mediaRepoMock.Object);

//            var service = new MediaFileService(unitOfWorkMock.Object, _mapper);

//            // Act
//            var result = await service.GetAllAsync();

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(2, result.Count());
//            Assert.Contains(result, f => f.FilePath == "file1.jpg");
//            Assert.All(result, item => Assert.IsType<MediaFileModel>(item));
//        }

//        [Fact]
//        public async Task GetByIdAsync_ReturnsCorrectMediaFileModel()
//        {
//            // Arrange
//            var media = new MediaFile { Id = 10, FilePath = "path.jpg", PlaceId = 5, FileType = Domain.Entities.Enums.FileType.Photo };

//            var mediaRepoMock = new Mock<IRepository<MediaFile>>();
//            mediaRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(media);

//            var unitOfWorkMock = new Mock<IUnitOfWork>();
//            unitOfWorkMock.Setup(u => u.MediaFiles).Returns(mediaRepoMock.Object);

//            var service = new MediaFileService(unitOfWorkMock.Object, _mapper);

//            // Act
//            var result = await service.GetByIdAsync(10);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal("path.jpg", result.FilePath);
//            Assert.Equal(5, result.PlaceId);
//        }

//        [Fact]
//        public async Task AddAsync_CallsAddAndSave()
//        {
//            // Arrange
//            var model = new MediaFileModel
//            {
//                FilePath = "test.jpg",
//                PlaceId = 1,
//                FileType = MediaFileModel.FileTypeEnum.Photo
//            };

//            var mediaRepoMock = new Mock<IRepository<MediaFile>>();
//            var unitOfWorkMock = new Mock<IUnitOfWork>();
//            unitOfWorkMock.Setup(u => u.MediaFiles).Returns(mediaRepoMock.Object);

//            var service = new MediaFileService(unitOfWorkMock.Object, _mapper);

//            // Act
//            await service.AddAsync(model);

//            // Assert
//            mediaRepoMock.Verify(r => r.AddAsync(It.IsAny<MediaFile>()), Times.Once);
//            unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
//        }
//    }
//}





////using Xunit;
////using Moq;
////using AutoMapper;
////using BLL.Models;
////using BLL.Services;
////using DAL.UnitOfWork;
////using Domain.Entities;
////using System.Collections.Generic;
////using System.Threading.Tasks;
////using System.Linq;

////namespace Tests
////{
////    public class MediaFileTests
////    {
////        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
////        private readonly IMapper _mapper;
////        private readonly MediaFileService _service;

////        public MediaFileTests()
////        {
////            _unitOfWorkMock = new Mock<IUnitOfWork>();

////            // Реальний AutoMapper профіль
////            var config = new MapperConfiguration(cfg =>
////            {
////                cfg.CreateMap<MediaFile, MediaFileModel>().ReverseMap();
////                cfg.CreateMap<Place, PlaceModel>().ReverseMap();
////            });
////            _mapper = config.CreateMapper();

////            _service = new MediaFileService(_unitOfWorkMock.Object, _mapper);
////        }

////        [Fact]
////        public async Task GetAllAsync_ReturnsMappedModels()
////        {
////            // Arrange
////            var mediaFiles = new List<MediaFile>
////            {
////                new MediaFile { Id = 1, FilePath = "file1.jpg", PlaceId = 1 },
////                new MediaFile { Id = 2, FilePath = "file2.jpg", PlaceId = 1 }
////            };
////            _unitOfWorkMock.Setup(u => u.MediaFiles.GetAllAsync())
////                .ReturnsAsync(mediaFiles);

////            // Act
////            var result = await _service.GetAllAsync();

////            // Assert
////            Assert.Equal(2, result.Count());
////            Assert.All(result, item => Assert.IsType<MediaFileModel>(item));
////        }

////        [Fact]
////        public async Task GetByIdAsync_ExistingId_ReturnsModel()
////        {
////            // Arrange
////            var mediaFile = new MediaFile { Id = 1, FilePath = "path.jpg", PlaceId = 1 };
////            _unitOfWorkMock.Setup(u => u.MediaFiles.GetByIdAsync(1)).ReturnsAsync(mediaFile);

////            // Act
////            var result = await _service.GetByIdAsync(1);

////            // Assert
////            Assert.NotNull(result);
////            Assert.Equal(mediaFile.Id, result.Id);
////            Assert.Equal(mediaFile.FilePath, result.FilePath);
////        }

////        [Fact]
////        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
////        {
////            _unitOfWorkMock.Setup(u => u.MediaFiles.GetByIdAsync(99)).ReturnsAsync((MediaFile?)null);

////            var result = await _service.GetByIdAsync(99);

////            Assert.Null(result);
////        }

////        [Fact]
////        public async Task AddAsync_ValidModel_CallsAddAndSave()
////        {
////            // Arrange
////            var model = new MediaFileModel { FilePath = "test.jpg", PlaceId = 1 };

////            // Act
////            await _service.AddAsync(model);

////            // Assert
////            _unitOfWorkMock.Verify(u => u.MediaFiles.AddAsync(It.IsAny<MediaFile>()), Times.Once);
////            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
////        }

////        [Fact]
////        public async Task DeleteAsync_ExistingId_RemovesAndSaves()
////        {
////            // Arrange
////            var entity = new MediaFile { Id = 1 };
////            _unitOfWorkMock.Setup(u => u.MediaFiles.GetByIdAsync(1)).ReturnsAsync(entity);

////            // Act
////            await _service.DeleteAsync(1);

////            // Assert
////            _unitOfWorkMock.Verify(u => u.MediaFiles.Remove(entity), Times.Once);
////            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
////        }

////        [Fact]
////        public async Task GetByPlaceIdAsync_ReturnsFilteredList()
////        {
////            // Arrange
////            var media = new List<MediaFile>
////            {
////                new MediaFile { Id = 1, PlaceId = 2 },
////                new MediaFile { Id = 2, PlaceId = 2 }
////            };
////            _unitOfWorkMock.Setup(u => u.MediaFiles.FindAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<MediaFile, bool>>>()))
////                .ReturnsAsync(media);

////            // Act
////            var result = await _service.GetByPlaceIdAsync(2);

////            // Assert
////            Assert.Equal(2, result.Count());
////        }
////    }
////}
