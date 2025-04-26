using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class VisitService : IVisitService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VisitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Visit>> GetAllAsync() =>
            await _unitOfWork.Visits.GetAllAsync();

        public async Task<Visit?> GetByIdAsync(int id) =>
            await _unitOfWork.Visits.GetByIdAsync(id);

        public async Task AddAsync(Visit visit)
        {
            await _unitOfWork.Visits.AddAsync(visit);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var v = await _unitOfWork.Visits.GetByIdAsync(id);
            if (v != null)
            {
                _unitOfWork.Visits.Remove(v);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<Visit>> GetByUserIdAsync(int userId) =>
            await _unitOfWork.Visits.FindAsync(v => v.UserId == userId);
    }
}