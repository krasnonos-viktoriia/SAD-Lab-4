using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.UnitOfWork;
using Domain.Entities;

namespace BLL.Services
{
    // Сервісний клас для роботи з місцями.
    public class PlaceService : IPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Ініціалізує сервіс через інтерфейс UnitOfWork.
        public PlaceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Асинхронно отримує всі місця.
        public async Task<IEnumerable<PlaceModel>> GetAllAsync()
        {
            var places = await _unitOfWork.Places.GetAllAsync();
            return _mapper.Map<IEnumerable<PlaceModel>>(places);
        }

        // Асинхронно отримує місце за його ідентифікатором.
        public async Task<PlaceModel?> GetByIdAsync(int id)
        {
            var place = await _unitOfWork.Places.GetByIdAsync(id);
            return place != null ? _mapper.Map<PlaceModel>(place) : null;
        }

        // Асинхронно додає нове місце.
        public async Task AddAsync(PlaceModel placeModel)
        {
            var place = _mapper.Map<Place>(placeModel);
            await _unitOfWork.Places.AddAsync(place);
            await _unitOfWork.SaveAsync();
        }

        // Асинхронно оновлює існуюче місце.
        public async Task UpdateAsync(PlaceModel placeModel)
        {
            var place = _mapper.Map<Place>(placeModel);
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