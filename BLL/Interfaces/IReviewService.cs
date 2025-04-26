using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
        Task<IEnumerable<Review>> GetByPlaceIdAsync(int placeId);
    }
}
