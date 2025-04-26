using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllAsync();
        Task<Question?> GetByIdAsync(int id);
        Task AddAsync(Question question);
        Task UpdateAsync(Question question);
        Task DeleteAsync(int id);
        Task AnswerQuestionAsync(int id, string answer);
    }
}
