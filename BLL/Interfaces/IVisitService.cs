using BLL.Models;
using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи з відвідуваннями місць користувачами.
    public interface IVisitService
    {
        // Асинхронно отримує всі відвідування.
        Task<IEnumerable<VisitModel>> GetAllAsync();

        // Асинхронно отримує відвідування за його ідентифікатором.
        Task<VisitModel?> GetByIdAsync(int id);

        // Асинхронно додає новий запис про відвідування.
        Task AddAsync(VisitModel visit);

        // Асинхронно видаляє запис про відвідування за ідентифікатором.
        Task DeleteAsync(int id);

        // Асинхронно отримує всі відвідування певного користувача.
        Task<IEnumerable<VisitModel>> GetByUserIdAsync(int userId);
    }
}