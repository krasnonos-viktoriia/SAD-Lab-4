using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи з медіафайлами.
    public interface IMediaFileService
    {
        // Асинхронно отримує всі медіафайли.
        Task<IEnumerable<MediaFile>> GetAllAsync();

        // Асинхронно отримує медіафайл за його ідентифікатором.
        Task<MediaFile?> GetByIdAsync(int id);

        // Асинхронно додає новий медіафайл.
        Task AddAsync(MediaFile file);

        // Асинхронно видаляє медіафайл за ідентифікатором.
        Task DeleteAsync(int id);

        // Асинхронно отримує всі медіафайли, що належать певному місцю.
        Task<IEnumerable<MediaFile>> GetByPlaceIdAsync(int placeId);
    }
}
