using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    // Сервіс для управління відвідинами, що включає додавання, видалення та отримання інформації про відвідування.
    public class VisitService : IVisitService
    {
        private readonly IUnitOfWork _unitOfWork;

        // Ініціалізація сервісу через інтерфейс UnitOfWork.
        public VisitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Отримання всіх відвідувань.
        public async Task<IEnumerable<Visit>> GetAllAsync() =>
            await _unitOfWork.Visits.GetAllAsync();

        // Отримання відвідувань за ідентифікатором користувача без завантаження додаткових зв'язків.
        public async Task<IEnumerable<Visit>> GetByUserIdAsync(int userId)
        {
            return await _unitOfWork.Set<Visit>()
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        // Отримання відвідувань за ідентифікатором користувача з жадібним завантаженням місць.
        public async Task<IEnumerable<Visit>> GetByUserIdWithIncludesAsync(int userId)
        {
            return await _unitOfWork.Set<Visit>()
                .Include(v => v.Place)  // Жадібне завантаження
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        // Отримання відвідування за ідентифікатором.
        public async Task<Visit?> GetByIdAsync(int id) =>
            await _unitOfWork.Visits.GetByIdAsync(id);

        // Додавання нового відвідування.
        public async Task AddAsync(Visit visit)
        {
            await _unitOfWork.Visits.AddAsync(visit);
            await _unitOfWork.SaveAsync();
        }

        // Видалення відвідування за ідентифікатором.
        public async Task DeleteAsync(int id)
        {
            var v = await _unitOfWork.Visits.GetByIdAsync(id);
            if (v != null)
            {
                _unitOfWork.Visits.Remove(v);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}