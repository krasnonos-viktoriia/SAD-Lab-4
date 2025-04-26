using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;

namespace BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Review>> GetAllAsync() =>
            await _unitOfWork.Reviews.GetAllAsync();

        public async Task<Review?> GetByIdAsync(int id) =>
            await _unitOfWork.Reviews.GetByIdAsync(id);

        public async Task AddAsync(Review review)
        {
            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _unitOfWork.Reviews.Update(review);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review != null)
            {
                _unitOfWork.Reviews.Remove(review);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<Review>> GetByPlaceIdAsync(int placeId) =>
            await _unitOfWork.Reviews.FindAsync(r => r.PlaceId == placeId);
    }
}
