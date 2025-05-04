using BLL.Models;
using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи з відгуками.
    public interface IReviewService
    {
        // Асинхронно отримує всі відгуки.
        Task<IEnumerable<ReviewModel>> GetAllAsync();

        // Асинхронно отримує відгук за його ідентифікатором.
        Task<ReviewModel?> GetByIdAsync(int id);

        // Асинхронно додає новий відгук.
        Task AddAsync(ReviewModel review);

        // Асинхронно оновлює існуючий відгук.
        Task UpdateAsync(ReviewModel review);

        // Асинхронно видаляє відгук за ідентифікатором.
        Task DeleteAsync(int id);

        // Асинхронно отримує всі відгуки для певного місця.
        Task<IEnumerable<ReviewModel>> GetByPlaceIdAsync(int placeId);
    }
}