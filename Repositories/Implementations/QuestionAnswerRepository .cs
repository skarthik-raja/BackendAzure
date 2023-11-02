using Microsoft.EntityFrameworkCore;
using SkillAssessment.Data;
using SkillAssessment.Models;

public class QuestionAnswerRepository : IQuestionAnswerRepository
{
    private readonly UserContext _context;

    public QuestionAnswerRepository(UserContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuestionAnswer>> GetAllQuestionAnswersAsync()
    {
        return await _context.questionAnswers.ToListAsync();
    }

    public async Task<QuestionAnswer> GetQuestionAnswerByIdAsync(int id)
    {
        return await _context.questionAnswers.FindAsync(id);
    }

    public async Task<bool> UpdateQuestionAnswerAsync(QuestionAnswer updatedQuestionAnswer)
    {
        var existingQuestionAnswer = await _context.questionAnswers.FindAsync(updatedQuestionAnswer.Id);

        if (existingQuestionAnswer == null)
            return false;

        existingQuestionAnswer.topic_id = updatedQuestionAnswer.topic_id;

        _context.Entry(existingQuestionAnswer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<QuestionAnswer> CreateQuestionAnswerAsync(QuestionAnswer questionAnswer)
    {
        _context.questionAnswers.Add(questionAnswer);
        await _context.SaveChangesAsync();
        return questionAnswer;
    }

    public async Task<bool> DeleteQuestionAnswerAsync(int id)
    {
        var questionAnswer = await _context.questionAnswers.FindAsync(id);
        if (questionAnswer == null)
            return false;

        _context.questionAnswers.Remove(questionAnswer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<QuestionAnswer>> GetQuestionAnswersByResultAsync(int resultId)
    {
        return await _context.questionAnswers
            .Where(qa => qa.topic_id == resultId)
            .ToListAsync();
    }
}
