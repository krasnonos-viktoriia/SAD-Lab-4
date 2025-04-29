using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;

namespace BLL.Services
{
    // Сервісний клас для роботи з місцями.
    public class PlaceService : IPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;

        // Ініціалізує сервіс через інтерфейс UnitOfWork.
        public PlaceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Асинхронно отримує всі місця.
        public async Task<IEnumerable<Place>> GetAllAsync()
        {
            return await _unitOfWork.Places.GetAllAsync();
        }

        // Асинхронно отримує місце за його ідентифікатором.
        public async Task<Place?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Places.GetByIdAsync(id);
        }

        // Асинхронно додає нове місце.
        public async Task AddAsync(Place place)
        {
            await _unitOfWork.Places.AddAsync(place);
            await _unitOfWork.SaveAsync();
        }

        // Асинхронно оновлює існуюче місце.
        public async Task UpdateAsync(Place place)
        {
            _unitOfWork.Places.Update(place);
            await _unitOfWork.SaveAsync();
        }

        // Асинхронно видаляє місце за ідентифікатором.
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