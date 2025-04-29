using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    // Сервіс для управління відгуками, включаючи додавання, оновлення, видалення та отримання відгуків.
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        // Ініціалізація сервісу через інтерфейс UnitOfWork.
        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Отримання всіх відгуків.
        public async Task<IEnumerable<Review>> GetAllAsync() =>
            await _unitOfWork.Reviews.GetAllAsync();

        // Отримання всіх відгуків з додатковими даними про місце та користувача.
        public async Task<IEnumerable<Review>> GetAllWithIncludesAsync()
        {
            return await _unitOfWork.Set<Review>()
                .Include(r => r.Place)  // Жадібне завантаження для властивості Place
                .Include(r => r.User)   // Жадібне завантаження для властивості User
                .ToListAsync();
        }

        // Отримання відгуку за ідентифікатором.
        public async Task<Review?> GetByIdAsync(int id) =>
            await _unitOfWork.Reviews.GetByIdAsync(id);

        // Додавання нового відгуку.
        public async Task AddAsync(Review review)
        {
            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveAsync();
        }

        // Оновлення існуючого відгуку.
        public async Task UpdateAsync(Review review)
        {
            _unitOfWork.Reviews.Update(review);
            await _unitOfWork.SaveAsync();
        }

        // Видалення відгуку за ідентифікатором.
        public async Task DeleteAsync(int id)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review != null)
            {
                _unitOfWork.Reviews.Remove(review);
                await _unitOfWork.SaveAsync();
            }
        }

        // Отримання всіх відгуків для конкретного місця.
        public async Task<IEnumerable<Review>> GetByPlaceIdAsync(int placeId) =>
            await _unitOfWork.Reviews.FindAsync(r => r.PlaceId == placeId);
    }
}