using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;

namespace BLL.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlaceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Place>> GetAllAsync()
        {
            return await _unitOfWork.Places.GetAllAsync();
        }

        public async Task<Place?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Places.GetByIdAsync(id);
        }

        public async Task AddAsync(Place place)
        {
            await _unitOfWork.Places.AddAsync(place);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Place place)
        {
            _unitOfWork.Places.Update(place);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var place = await _unitOfWork.Places.GetByIdAsync(id);
            if (place != null)
            {
                _unitOfWork.Places.Remove(place);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
