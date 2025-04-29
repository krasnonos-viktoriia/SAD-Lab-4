using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;

namespace BLL.Services
{
    // Сервіс для управління користувачами, що включає додавання, оновлення, видалення та отримання інформації про користувачів.
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        // Ініціалізація сервісу через інтерфейс UnitOfWork.
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Отримання всіх користувачів.
        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _unitOfWork.Users.GetAllAsync();

        // Отримання користувача за ідентифікатором.
        public async Task<User?> GetByIdAsync(int id) =>
            await _unitOfWork.Users.GetByIdAsync(id);

        // Додавання нового користувача.
        public async Task AddAsync(User user)
        {
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
        }

        // Оновлення існуючого користувача.
        public async Task UpdateAsync(User user)
        {
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
        }

        // Видалення користувача за ідентифікатором.
        public async Task DeleteAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user != null)
            {
                _unitOfWork.Users.Remove(user);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}