using BLL.Models;
using Domain.Entities;

namespace BLL.Interfaces
{
    // Сервісний інтерфейс для роботи із запитаннями користувачів.
    public interface IQuestionService
    {
        // Асинхронно отримує всі запитання.
        Task<IEnumerable<QuestionModel>> GetAllAsync();

        // Асинхронно отримує запитання за його ідентифікатором.
        Task<QuestionModel?> GetByIdAsync(int id);

        // Асинхронно додає нове запитання.
        Task AddAsync(QuestionModel question);

        // Асинхронно оновлює запитання.
        Task UpdateAsync(QuestionModel question);

        // Асинхронно видаляє запитання за ідентифікатором.
        Task DeleteAsync(int id);

        // Асинхронно додає відповідь до запитання за його ідентифікатором.
        Task AnswerQuestionAsync(int id, string answer);
    }
}