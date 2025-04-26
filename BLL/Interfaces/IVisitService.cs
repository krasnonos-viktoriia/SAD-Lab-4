using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IVisitService
    {
        Task<IEnumerable<Visit>> GetAllAsync();
        Task<Visit?> GetByIdAsync(int id);
        Task AddAsync(Visit visit);
        Task DeleteAsync(int id);
        Task<IEnumerable<Visit>> GetByUserIdAsync(int userId);
    }
}