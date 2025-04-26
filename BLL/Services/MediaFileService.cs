using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;

namespace BLL.Services
{
    public class MediaFileService : IMediaFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MediaFileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MediaFile>> GetAllAsync() =>
            await _unitOfWork.MediaFiles.GetAllAsync();

        public async Task<MediaFile?> GetByIdAsync(int id) =>
            await _unitOfWork.MediaFiles.GetByIdAsync(id);

        public async Task AddAsync(MediaFile file)
        {
            await _unitOfWork.MediaFiles.AddAsync(file);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var f = await _unitOfWork.MediaFiles.GetByIdAsync(id);
            if (f != null)
            {
                _unitOfWork.MediaFiles.Remove(f);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<MediaFile>> GetByPlaceIdAsync(int placeId) =>
            await _unitOfWork.MediaFiles.FindAsync(f => f.PlaceId == placeId);
    }

}
