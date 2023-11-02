using SkillAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAssessment.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<IEnumerable<User>> GetUsersByEmailAsync(string userEmail);
        Task<int?> GetUserIdByEmailAsync(string userEmail);
        Task<IEnumerable<string>> GetDepartmentsAsync();
    }
}
