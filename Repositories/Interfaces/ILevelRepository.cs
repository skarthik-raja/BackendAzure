using SkillAssessment.Models;

public interface ILevelRepository
{
    Task<IEnumerable<Level>> GetLevelsAsync();
    Task<Level> GetLevelByIdAsync(int id);
    Task<Level> AddLevelAsync(Level level);
    Task<bool> UpdateLevelAsync(Level level);
    Task<bool> DeleteLevelAsync(int id);
}
