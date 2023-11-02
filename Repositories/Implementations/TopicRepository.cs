using Microsoft.EntityFrameworkCore;
using SkillAssessment.Data;
using SkillAssessment.Models;
using SkillAssessment.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAssessment.Repositories.Implementations
{
    public class TopicRepository : ITopicRepository
    {
        private readonly UserContext _context;

        public TopicRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Topics>> GetTopicsAsync()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<Topics> GetTopicByNameAsync(string topicName)
        {
            return await _context.Topics.Include(t => t.Questions).FirstOrDefaultAsync(t => t.Topic_Name == topicName);
        }

        public async Task CreateTopicAsync(Topics topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
        }
    }
}
