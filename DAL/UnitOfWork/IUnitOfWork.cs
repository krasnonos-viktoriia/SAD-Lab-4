using DAL.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.UnitOfWork
{
    // Інтерфейс для одиничної транзакції (Unit of Work), що забезпечує доступ до репозиторіїв та збереження змін в базі даних.
    public interface IUnitOfWork : IDisposable
    {
        // Репозиторій для роботи з сутностями типу User.
        IRepository<User> Users { get; }

        // Репозиторій для роботи з сутностями типу Place.
        IRepository<Place> Places { get; }

        // Репозиторій для роботи з сутностями типу Review.
        IRepository<Review> Reviews { get; }

        // Репозиторій для роботи з сутностями типу Question.
        IRepository<Question> Questions { get; }

        // Репозиторій для роботи з сутностями типу Visit.
        IRepository<Visit> Visits { get; }

        // Репозиторій для роботи з сутностями типу MediaFile.
        IRepository<MediaFile> MediaFiles { get; }

        // Загальний метод для отримання репозиторія для конкретної сутності.
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        // Метод для асинхронного збереження змін в базі даних.
        Task<int> SaveAsync();
    }
}