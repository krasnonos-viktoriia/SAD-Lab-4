using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IMediaFileService
    {
        Task<IEnumerable<MediaFile>> GetAllAsync();
        Task<MediaFile?> GetByIdAsync(int id);
        Task AddAsync(MediaFile file);
        Task DeleteAsync(int id);
        Task<IEnumerable<MediaFile>> GetByPlaceIdAsync(int placeId);
    }
}
