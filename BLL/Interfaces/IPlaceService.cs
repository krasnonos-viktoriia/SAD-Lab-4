using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи з об'єктами місць.
    public interface IPlaceService
    {
        // Асинхронно отримує всі місця.
        Task<IEnumerable<Place>> GetAllAsync();

        // Асинхронно отримує місце за його ідентифікатором.
        Task<Place?> GetByIdAsync(int id);

        // Асинхронно додає нове місце.
        Task AddAsync(Place place);

        // Асинхронно оновлює дані місця.
        Task UpdateAsync(Place place);

        // Асинхронно видаляє місце за ідентифікатором.
        Task DeleteAsync(int id);
    }
}
