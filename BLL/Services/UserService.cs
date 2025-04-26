using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _unitOfWork.Users.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id) =>
            await _unitOfWork.Users.GetByIdAsync(id);

        public async Task AddAsync(User user)
        {
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
        }

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
