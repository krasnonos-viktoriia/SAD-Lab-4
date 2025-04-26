using BLL.Interfaces;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Question>> GetAllAsync() =>
            await _unitOfWork.Questions.GetAllAsync();

        public async Task<IEnumerable<Question>> GetAllWithIncludesAsync()
        {
            return await _unitOfWork.Set<Question>()
                .Include(q => q.Place)
                .Include(q => q.User)
                .ToListAsync();
        }

        public async Task<Question?> GetByIdAsync(int id) =>
            await _unitOfWork.Questions.GetByIdAsync(id);

        public async Task AddAsync(Question question)
        {
            await _unitOfWork.Questions.AddAsync(question);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Question question)
        {
            _unitOfWork.Questions.Update(question);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var q = await _unitOfWork.Questions.GetByIdAsync(id);
            if (q != null)
            {
                _unitOfWork.Questions.Remove(q);
                await _unitOfWork.SaveAsync();
            }
        }

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
