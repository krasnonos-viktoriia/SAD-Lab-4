using BLL.Models;
using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи з об'єктами місць.
    public interface IPlaceService
    {
        // Асинхронно отримує всі місця.
        Task<IEnumerable<PlaceModel>> GetAllAsync();

        // Асинхронно отримує місце за його ідентифікатором.
        Task<PlaceModel?> GetByIdAsync(int id);

        // Асинхронно додає нове місце.
        Task AddAsync(PlaceModel place);

        // Асинхронно оновлює дані місця.
        Task UpdateAsync(PlaceModel place);

        // Асинхронно видаляє місце за ідентифікатором.
        Task DeleteAsync(int id);
    }
}
