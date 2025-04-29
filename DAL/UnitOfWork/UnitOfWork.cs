using DAL.Repositories;
using Domain.Entities;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.UnitOfWork
{
    // Клас, що реалізує патерн "Одинична транзакція" (Unit of Work) і надає доступ до репозиторіїв для всіх сутностей.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Репозиторії для роботи з різними сутностями.
        public IRepository<User> Users { get; }
        public IRepository<Place> Places { get; }
        public IRepository<Review> Reviews { get; }
        public IRepository<Question> Questions { get; }
        public IRepository<Visit> Visits { get; }
        public IRepository<MediaFile> MediaFiles { get; }

        // Конструктор ініціалізує репозиторії з контекстом бази даних.
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new Repository<User>(context);
            Places = new Repository<Place>(context);
            Reviews = new Repository<Review>(context);
            Questions = new Repository<Question>(context);
            Visits = new Repository<Visit>(context);
            MediaFiles = new Repository<MediaFile>(context);
        }

        // Метод для збереження змін у базі даних.
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Метод для отримання DbSet для конкретної сутності.
        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        // Метод для звільнення ресурсів.
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}