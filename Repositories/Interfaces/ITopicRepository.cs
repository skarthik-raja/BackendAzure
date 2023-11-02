using SkillAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAssessment.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Topics>> GetTopicsAsync();
        Task<Topics> GetTopicByNameAsync(string topicName);
        Task CreateTopicAsync(Topics topic);
    }
}
