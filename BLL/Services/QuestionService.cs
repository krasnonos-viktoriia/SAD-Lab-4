using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    // Сервіс для управління питаннями, включаючи додавання, оновлення, видалення та відповіді на питання.
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Ініціалізація сервісу через інтерфейс UnitOfWork.
        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Отримання всіх питань.
        public async Task<IEnumerable<QuestionModel>> GetAllAsync()
        {
            var question = await _unitOfWork.Questions.GetAllAsync();
            return _mapper.Map<IEnumerable<QuestionModel>>(question);
        }

        // Отримання всіх питань з додатковими даними про місце і користувача.
        public async Task<IEnumerable<QuestionModel>> GetAllWithIncludesAsync()
        {
            var question = await _unitOfWork.Set<Question>()
               .Include(q => q.Place)  // Жадібне завантаження для властивості Place
               .Include(q => q.User)   // Жадібне завантаження для властивості User
               .ToListAsync();
            return _mapper.Map<IEnumerable<QuestionModel>>(question);
        }

        // Отримання питання за ідентифікатором.
        public async Task<QuestionModel?> GetByIdAsync(int id)
        {
            var question = await _unitOfWork.Questions.GetByIdAsync(id);
            return question == null ? null : _mapper.Map<QuestionModel>(question);
        }

        // Додавання нового питання.
        public async Task AddAsync(QuestionModel questionModel)
        {
            var question = _mapper.Map<Question>(questionModel);
            await _unitOfWork.Questions.AddAsync(question);
            await _unitOfWork.SaveAsync();
        }

        // Оновлення існуючого питання.
        public async Task UpdateAsync(QuestionModel questionModel)
        {
            var question = _mapper.Map<Question>(questionModel);
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