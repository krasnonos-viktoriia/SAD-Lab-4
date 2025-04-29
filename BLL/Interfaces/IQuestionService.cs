using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи із запитаннями користувачів.
    public interface IQuestionService
    {
        // Асинхронно отримує всі запитання.
        Task<IEnumerable<Question>> GetAllAsync();

        // Асинхронно отримує запитання за його ідентифікатором.
        Task<Question?> GetByIdAsync(int id);

        // Асинхронно додає нове запитання.
        Task AddAsync(Question question);

        // Асинхронно оновлює запитання.
        Task UpdateAsync(Question question);

        // Асинхронно видаляє запитання за ідентифікатором.
        Task DeleteAsync(int id);

        // Асинхронно додає відповідь до запитання за його ідентифікатором.
        Task AnswerQuestionAsync(int id, string answer);
    }
}