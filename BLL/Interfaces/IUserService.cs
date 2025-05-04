using BLL.Models;
using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи з користувачами.
    public interface IUserService
    {
        // Асинхронно отримує всіх користувачів.
        Task<IEnumerable<UserModel>> GetAllAsync();

        // Асинхронно отримує користувача за його ідентифікатором.
        Task<UserModel?> GetByIdAsync(int id);

        // Асинхронно додає нового користувача.
        Task AddAsync(UserModel user);

        // Асинхронно оновлює існуючого користувача.
        Task UpdateAsync(UserModel user);

        // Асинхронно видаляє користувача за ідентифікатором.
        Task DeleteAsync(int id);
    }
}