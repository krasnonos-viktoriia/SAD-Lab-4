using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IPlaceService
    {
        Task<IEnumerable<Place>> GetAllAsync();
        Task<Place?> GetByIdAsync(int id);
        Task AddAsync(Place place);
        Task UpdateAsync(Place place);
        Task DeleteAsync(int id);
    }
}
