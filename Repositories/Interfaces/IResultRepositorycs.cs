using SkillAssessment.Models;

public interface IResultRepository
{
    Task<IEnumerable<Result>> GetAllResultsAsync();
    Task<Result> GetResultByIdAsync(int id);
    Task<Result> CreateResultAsync(Result result);
    Task<bool> DeleteResultAsync(int id);
    Task<IEnumerable<Result>> GetResultsByUserIdAsync(int userId);
    Task<int> GetResultCountByUserIdAsync(int userId);
    Task<int> GetTotalPointsByUserIdAsync(int userId);
}
