using SkillAssessment.Models;

public interface IQuestionAnswerRepository
{
    Task<IEnumerable<QuestionAnswer>> GetAllQuestionAnswersAsync();
    Task<QuestionAnswer> GetQuestionAnswerByIdAsync(int id);
    Task<bool> UpdateQuestionAnswerAsync(QuestionAnswer updatedQuestionAnswer);
    Task<QuestionAnswer> CreateQuestionAnswerAsync(QuestionAnswer questionAnswer);
    Task<bool> DeleteQuestionAnswerAsync(int id);
    Task<IEnumerable<QuestionAnswer>> GetQuestionAnswersByResultAsync(int resultId);
}
