using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи з користувачами.
    public interface IUserService
    {
        // Асинхронно отримує всіх користувачів.
        Task<IEnumerable<User>> GetAllAsync();

        // Асинхронно отримує користувача за його ідентифікатором.
        Task<User?> GetByIdAsync(int id);

        // Асинхронно додає нового користувача.
        Task AddAsync(User user);

        // Асинхронно оновлює існуючого користувача.
        Task UpdateAsync(User user);

        // Асинхронно видаляє користувача за ідентифікатором.
        Task DeleteAsync(int id);
    }
}