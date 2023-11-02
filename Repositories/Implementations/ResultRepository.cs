using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillAssessment.Models;
using SkillAssessment.Data;

namespace SkillAssessment.Repositories.Implementations
{
    public class ResultRepository : IResultRepository
    {
        private readonly UserContext _context;

        public ResultRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Result>> GetAllResultsAsync()
        {
            return await _context.Results.Include(r => r.users).ToListAsync();
        }

        public async Task<Result> GetResultByIdAsync(int id)
        {
            return await _context.Results
                .Include(r => r.Topics)
                .Include(r => r.users)
                .Include(r => r.assessment)
                .FirstOrDefaultAsync(r => r.result_id == id);
        }

        public async Task<Result> CreateResultAsync(Result result)
        {
            // Calculate pass or fail based on the values
            if (result.AnsweredQuestions >= result.TotalQuestions / 2)
            {
                result.passorfail = "Pass";
            }
            else
            {
                result.passorfail = "Fail";
            }

            var user = await _context.Users.FindAsync(result.users.User_ID);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            result.users = user;

            var assessment = await _context.Assessments.FindAsync(result.assessment.Assessment_ID);
            if (assessment == null)
            {
                throw new ArgumentException("Invalid assessment ID");
            }
            result.assessment = assessment;

            var topic = await _context.Topics.FindAsync(result.Topics.Topic_Id);
            if (topic == null)
            {
                throw new ArgumentException("Invalid topic ID");
            }
            result.Topics = topic;

            // Set the date to today's date
            result.date = DateTime.Today.ToString("yyyy-MM-dd");

            _context.Results.Add(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteResultAsync(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null)
                return false;

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Result>> GetResultsByUserIdAsync(int userId)
        {
            return await _context.Results
                .Where(r => r.users.User_ID == userId)
                .Include(r => r.users)
                .Include(r => r.assessment)
                .ToListAsync();
        }

        public async Task<int> GetResultCountByUserIdAsync(int userId)
        {
            return await _context.Results
                .Where(r => r.users.User_ID == userId)
                .CountAsync();
        }

        public async Task<int> GetTotalPointsByUserIdAsync(int userId)
        {
            var totalPoints = await _context.Results
                .Where(r => r.users.User_ID == userId)
                .SumAsync(r => r.points);

            return totalPoints;
        }
    }
}
