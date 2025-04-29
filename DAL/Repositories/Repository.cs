using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DAL.Data;

namespace DAL.Repositories
{
    // Загальний репозиторій для роботи з сутностями типу T в базі даних, реалізує інтерфейс IRepository.
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        // Конструктор для ініціалізації контексту бази даних та DbSet для сутностей типу T.
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Отримує всі сутності типу T з бази даних асинхронно.
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Отримує сутність за її ідентифікатором асинхронно.
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Виконує пошук сутностей, що відповідають умові, асинхронно.
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // Додає нову сутність до бази даних асинхронно.
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Видаляє сутність з бази даних.
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        // Оновлює сутність в базі даних.
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}