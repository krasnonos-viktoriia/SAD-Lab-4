using System.Linq.Expressions;

namespace DAL.Repositories
{
    // Загальний інтерфейс для репозиторіїв, який надає методи для роботи з сутностями: отримання, додавання, видалення та оновлення.
    public interface IRepository<T> where T : class
    {
        // Отримує всі сутності типу T асинхронно.
        Task<IEnumerable<T>> GetAllAsync();

        // Отримує сутність за ідентифікатором асинхронно.
        Task<T?> GetByIdAsync(int id);

        // Виконує пошук сутностей, що відповідають умові.
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Додає нову сутність асинхронно.
        Task AddAsync(T entity);

        // Видаляє сутність.
        void Remove(T entity);

        // Оновлює сутність.
        void Update(T entity);
    }
}