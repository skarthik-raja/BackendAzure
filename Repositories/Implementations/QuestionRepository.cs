using Microsoft.EntityFrameworkCore;
using SkillAssessment.Data;
using SkillAssessment.Models;

namespace SkillAssessment.Repositories.Implementations
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly UserContext _context;

        public QuestionRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Questions>> GetAllQuestionsAsync()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Questions> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<bool> UpdateQuestionAsync(Questions updatedQuestion)
        {
            _context.Entry(updatedQuestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<Questions> CreateQuestionAsync(Questions question)
        {
            var topic = await _context.Topics.FindAsync(question.topics.Topic_Id);
            question.topics = topic;

            var level = await _context.levels.FindAsync(question.Levels.LevelId);
            question.Levels= level;

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
                return false;

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Questions>> GetQuestionsByTopicAsync(int topicId)
        {
            return await _context.Questions
                .Where(q => q.topics.Topic_Id == topicId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Questions>> GetQuestionsByTopicAndLevelAsync(int topicId, int levelId)
        {
            return await _context.Questions
                .Where(q => q.topics.Topic_Id == topicId && q.Levels.LevelId == levelId)
                .ToListAsync();
        }

    }

}
