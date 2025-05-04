using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace BLL.Services
{
    // Сервісний клас для роботи з медіафайлами.
    public class MediaFileService : IMediaFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Ініціалізує сервіс через інтерфейс UnitOfWork.
        public MediaFileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Асинхронно отримує всі медіафайли.
        public async Task<IEnumerable<MediaFileModel>> GetAllAsync()
         {
            var files = await _unitOfWork.MediaFiles.GetAllAsync();
            return _mapper.Map<IEnumerable<MediaFileModel>>(files);
        }

        // Асинхронно отримує медіафайл за його ідентифікатором.
        public async Task<MediaFileModel?> GetByIdAsync(int id)
         {
            var file = await _unitOfWork.MediaFiles.GetByIdAsync(id);
            return file != null ? _mapper.Map<MediaFileModel>(file) : null;
        }

        // Асинхронно додає новий медіафайл.
        public async Task AddAsync(MediaFileModel fileModel)
        {
            var file = _mapper.Map<MediaFile>(fileModel);
            await _unitOfWork.MediaFiles.AddAsync(file);
            await _unitOfWork.SaveAsync();
        }

        // Асинхронно видаляє медіафайл за ідентифікатором.
        public async Task DeleteAsync(int id)
        {
            var f = await _unitOfWork.MediaFiles.GetByIdAsync(id);
            if (f != null)
            {
                _unitOfWork.MediaFiles.Remove(f);
                await _unitOfWork.SaveAsync();
            }
        }

        // Асинхронно отримує медіафайли для певного місця.
        public async Task<IEnumerable<MediaFileModel>> GetByPlaceIdAsync(int placeId)
        {
            var files = await _unitOfWork.MediaFiles.FindAsync(f => f.PlaceId == placeId);
            return _mapper.Map<IEnumerable<MediaFileModel>>(files);
        }
}
}