using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillAssessment.Data;
using SkillAssessment.Models;

namespace SkillAssessment.Repositories.Implementations
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly UserContext _context;

        public AnswerRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Answer>> GetAllAnswersAsync()
        {
            return await _context.answer.ToListAsync();
        }

        public async Task<Answer> GetAnswerByIdAsync(int id)
        {
            return await _context.answer.FindAsync(id);
        }

        public async Task<bool> UpdateAnswerAsync(int id, Answer answer)
        {
            if (id != answer.Id)
            {
                return false;
            }

            _context.Entry(answer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return AnswerExists(id);
            }
        }

        public async Task<Answer> CreateAnswerAsync(Answer answer)
        {
            _context.answer.Add(answer);

            if (answer.QuestionAnswers != null && answer.QuestionAnswers.Any())
            {
                foreach (var questionAnswer in answer.QuestionAnswers)
                {
                    _context.questionAnswers.Add(questionAnswer);
                }
            }

            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<bool> DeleteAnswerAsync(int id)
        {
            var answer = await _context.answer.FindAsync(id);
            if (answer == null)
            {
                return false;
            }

            _context.answer.Remove(answer);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool AnswerExists(int id)
        {
            return _context.answer.Any(e => e.Id == id);
        }
    }

}
