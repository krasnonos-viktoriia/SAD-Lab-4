using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.UnitOfWork;
using Domain.Entities;

namespace BLL.Services
{
    // Сервіс для управління користувачами, що включає додавання, оновлення, видалення та отримання інформації про користувачів.
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Ініціалізація сервісу через інтерфейс UnitOfWork.
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Отримання всіх користувачів.
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
                var users = await _unitOfWork.Users.GetAllAsync();
                return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        // Отримання користувача за ідентифікатором.
        public async Task<UserModel?> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return user != null ? _mapper.Map<UserModel>(user) : null;
        }

        // Додавання нового користувача.
        public async Task AddAsync(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
        }

        // Оновлення існуючого користувача.
        public async Task UpdateAsync(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
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