using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    // Сервіс для управління відвідинами, що включає додавання, видалення та отримання інформації про відвідування.
    public class VisitService : IVisitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Ініціалізація сервісу через інтерфейс UnitOfWork.
        public VisitService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Отримання всіх відвідувань.
        public async Task<IEnumerable<VisitModel>> GetAllAsync()
          {
            var visit = await _unitOfWork.Visits.GetAllAsync();
            return _mapper.Map<IEnumerable<VisitModel>>(visit);
        }

        // Отримання відвідувань за ідентифікатором користувача без завантаження додаткових зв'язків.
        public async Task<IEnumerable<VisitModel>> GetByUserIdAsync(int userId)
        {
            var visit = await _unitOfWork.Set<Visit>()
                .Where(v => v.UserId == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<VisitModel>>(visit);
        }

        // Отримання відвідувань за ідентифікатором користувача з жадібним завантаженням місць.
        public async Task<IEnumerable<VisitModel>> GetByUserIdWithIncludesAsync(int userId)
        {
            var visit = await _unitOfWork.Set<Visit>()
                .Include(v => v.Place)  // Жадібне завантаження
                .Where(v => v.UserId == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<VisitModel>>(visit);
        }

        // Отримання відвідування за ідентифікатором.
        public async Task<VisitModel?> GetByIdAsync(int id)
        {
            var visit = await _unitOfWork.Visits.GetByIdAsync(id);
            return visit == null ? null : _mapper.Map<VisitModel>(visit);
        }

        // Додавання нового відвідування.
        public async Task AddAsync(VisitModel visitModel)
        {
            var visit = _mapper.Map<Visit>(visitModel);
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