using SkillAssessment.Models;

public interface IQuestionRepository
{
    Task<IEnumerable<Questions>> GetAllQuestionsAsync();
    Task<Questions> GetQuestionByIdAsync(int id);
    Task<bool> UpdateQuestionAsync(Questions updatedQuestion);
    Task<Questions> CreateQuestionAsync(Questions question);
    Task<bool> DeleteQuestionAsync(int id);
    Task<IEnumerable<Questions>> GetQuestionsByTopicAsync(int topicId);
    Task<IEnumerable<Questions>> GetQuestionsByTopicAndLevelAsync(int topicId, int levelId);

}
