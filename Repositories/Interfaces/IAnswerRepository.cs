using SkillAssessment.Models;

public interface IAnswerRepository
{
    Task<IEnumerable<Answer>> GetAllAnswersAsync();
    Task<Answer> GetAnswerByIdAsync(int id);
    Task<bool> UpdateAnswerAsync(int id, Answer answer);
    Task<Answer> CreateAnswerAsync(Answer answer);
    Task<bool> DeleteAnswerAsync(int id);
}
