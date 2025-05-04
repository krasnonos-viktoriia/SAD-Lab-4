using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    // Сервіс для управління відгуками, включаючи додавання, оновлення, видалення та отримання відгуків.
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Ініціалізація сервісу через інтерфейс UnitOfWork.
        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Отримання всіх відгуків.
        public async Task<IEnumerable<ReviewModel>> GetAllAsync()
        {
            var reviews = await _unitOfWork.Reviews.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewModel>>(reviews);
        }

        // Отримання всіх відгуків з додатковими даними про місце та користувача.
        public async Task<IEnumerable<ReviewModel>> GetAllWithIncludesAsync()
        {
            var reviews = await _unitOfWork.Set<Review>()
               .Include(r => r.Place)  // Жадібне завантаження для властивості Place
               .Include(r => r.User)   // Жадібне завантаження для властивості User
               .ToListAsync();
            return _mapper.Map<IEnumerable<ReviewModel>>(reviews);
        }

        // Отримання відгуку за ідентифікатором.
        public async Task<ReviewModel?> GetByIdAsync(int id)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            return review != null ? _mapper.Map<ReviewModel>(review) : null;
        }

        // Додавання нового відгуку.
        public async Task AddAsync(ReviewModel reviewModel)
        {
            var review = _mapper.Map<Review>(reviewModel);
            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveAsync();
        }

        // Оновлення існуючого відгуку.
        public async Task UpdateAsync(ReviewModel reviewModel)
        {
            var review = _mapper.Map<Review>(reviewModel);
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
        public async Task<IEnumerable<ReviewModel>> GetByPlaceIdAsync(int placeId)
        {
            var reviews = await _unitOfWork.Reviews.FindAsync(r => r.PlaceId == placeId);
            return _mapper.Map<IEnumerable<ReviewModel>>(reviews);
        }
    }
}