using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    // Сервісний клас для роботи з медіафайлами.
    public class MediaFileService : IMediaFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        // Ініціалізує сервіс через інтерфейс UnitOfWork.
        public MediaFileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Асинхронно отримує всі медіафайли.
        public async Task<IEnumerable<MediaFile>> GetAllAsync() =>
            await _unitOfWork.MediaFiles.GetAllAsync();

        // Асинхронно отримує медіафайл за його ідентифікатором.
        public async Task<MediaFile?> GetByIdAsync(int id) =>
            await _unitOfWork.MediaFiles.GetByIdAsync(id);

        // Асинхронно додає новий медіафайл.
        public async Task AddAsync(MediaFile file)
        {
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
        public async Task<IEnumerable<MediaFile>> GetByPlaceIdAsync(int placeId) =>
            await _unitOfWork.MediaFiles.FindAsync(f => f.PlaceId == placeId);
    }
}