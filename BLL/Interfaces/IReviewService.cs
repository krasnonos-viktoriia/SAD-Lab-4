using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи з відгуками.
    public interface IReviewService
    {
        // Асинхронно отримує всі відгуки.
        Task<IEnumerable<Review>> GetAllAsync();

        // Асинхронно отримує відгук за його ідентифікатором.
        Task<Review?> GetByIdAsync(int id);

        // Асинхронно додає новий відгук.
        Task AddAsync(Review review);

        // Асинхронно оновлює існуючий відгук.
        Task UpdateAsync(Review review);

        // Асинхронно видаляє відгук за ідентифікатором.
        Task DeleteAsync(int id);

        // Асинхронно отримує всі відгуки для певного місця.
        Task<IEnumerable<Review>> GetByPlaceIdAsync(int placeId);
    }
}