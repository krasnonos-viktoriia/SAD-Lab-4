using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    // Сервіс для управління питаннями, включаючи додавання, оновлення, видалення та відповіді на питання.
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        // Ініціалізація сервісу через інтерфейс UnitOfWork.
        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Отримання всіх питань.
        public async Task<IEnumerable<Question>> GetAllAsync() =>
            await _unitOfWork.Questions.GetAllAsync();

        // Отримання всіх питань з додатковими даними про місце і користувача.
        public async Task<IEnumerable<Question>> GetAllWithIncludesAsync()
        {
            return await _unitOfWork.Set<Question>()
                .Include(q => q.Place)  // Жадібне завантаження для властивості Place
                .Include(q => q.User)   // Жадібне завантаження для властивості User
                .ToListAsync();
        }

        // Отримання питання за ідентифікатором.
        public async Task<Question?> GetByIdAsync(int id) =>
            await _unitOfWork.Questions.GetByIdAsync(id);

        // Додавання нового питання.
        public async Task AddAsync(Question question)
        {
            await _unitOfWork.Questions.AddAsync(question);
            await _unitOfWork.SaveAsync();
        }

        // Оновлення існуючого питання.
        public async Task UpdateAsync(Question question)
        {
            _unitOfWork.Questions.Update(question);
            await _unitOfWork.SaveAsync();
        }

        // Видалення питання за ідентифікатором.
        public async Task DeleteAsync(int id)
        {
            var q = await _unitOfWork.Questions.GetByIdAsync(id);
            if (q != null)
            {
                _unitOfWork.Questions.Remove(q);
                await _unitOfWork.SaveAsync();
            }
        }

        // Відповідь на питання.
        public async Task AnswerQuestionAsync(int id, string answer)
        {
            var question = await _unitOfWork.Questions.GetByIdAsync(id);
            if (question != null)
            {
                question.Answer = answer;
                _unitOfWork.Questions.Update(question);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}