using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи з відвідуваннями місць користувачами.
    public interface IVisitService
    {
        // Асинхронно отримує всі відвідування.
        Task<IEnumerable<Visit>> GetAllAsync();

        // Асинхронно отримує відвідування за його ідентифікатором.
        Task<Visit?> GetByIdAsync(int id);

        // Асинхронно додає новий запис про відвідування.
        Task AddAsync(Visit visit);

        // Асинхронно видаляє запис про відвідування за ідентифікатором.
        Task DeleteAsync(int id);

        // Асинхронно отримує всі відвідування певного користувача.
        Task<IEnumerable<Visit>> GetByUserIdAsync(int userId);
    }
}