using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillAssessment.Data;
using SkillAssessment.Models;

public class AssessmentRepository : IAssessmentRepository
{
    private readonly UserContext _context;

    public AssessmentRepository(UserContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Assessment>> GetAllAssessments()
    {
        return await _context.Assessments.ToListAsync();
    }

    public async Task<Assessment> GetAssessmentById(int assessmentId)
    {
        return await _context.Assessments.FindAsync(assessmentId);
    }

    public async Task<Assessment> CreateAssessment(Assessment assessment)
    {
        // Implement the logic to create an assessment
        assessment.Assessment_Points = assessment.Assessment_NoOfQuestions * 10;
        assessment.Assessment_Requested_Date = DateTime.Today;
        assessment.Assessment_DateOfCompletion = DateTime.Today.AddDays(10);

        var user = await _context.Users.FindAsync(assessment.Users.User_ID);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        assessment.Users = user;

        var topic = await _context.Topics.FindAsync(assessment.Topics.Topic_Id);
        if (topic == null)
        {
            throw new KeyNotFoundException("Topic not found.");
        }
        assessment.Topics = topic;



        var level = await _context.levels.FindAsync(assessment.Levels.LevelId);
        if (level == null)
        {
            throw new KeyNotFoundException("level not found.");
        }
        assessment.Levels = level;



        assessment.Assessment_type = "not finished";

        _context.Assessments.Add(assessment);
        await _context.SaveChangesAsync();

        return assessment;
    }

    public async Task<Assessment> UpdateAssessment(int assessmentId, string newAssessmentType)
    {
        var assessment = await _context.Assessments.FindAsync(assessmentId);

        if (assessment == null)
        {
            throw new KeyNotFoundException("Assessment not found.");
        }

        var oldAssessmentType = assessment.Assessment_type;
        assessment.Assessment_type = newAssessmentType;

        try
        {
            _context.Entry(assessment).Property(a => a.Assessment_type).IsModified = true;
            await _context.SaveChangesAsync();
            assessment.Assessment_type = oldAssessmentType;
            return assessment;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AssessmentExists(assessmentId))
            {
                throw new KeyNotFoundException("Assessment not found.");
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<IEnumerable<Assessment>> GetUnfinishedAssessmentsForUser(int userId)
    {
        return await _context.Assessments
            .Where(a => a.Users.User_ID == userId && a.Assessment_type == "not finished")
            .Include(x => x.Topics)
            .ToListAsync();
    }

    public async Task<int> GetNumberOfUnfinishedAssessmentsForUser(int userId)
    {
        int count = await _context.Assessments
            .Where(a => a.Users.User_ID == userId && a.Assessment_type == "not finished")
            .CountAsync();

        return count;
    }

    public async Task<Assessment> GetMaxAssessment(int userId)
    {
        return await _context.Assessments
            .Where(a => a.Users.User_ID == userId)
            .OrderByDescending(a => a.Assessment_ID)
            .FirstOrDefaultAsync();
    }

    public async Task<object> GetAssessmentDetails(int userId)
    {
        return await _context.Users
            .Where(u => u.User_ID == userId)
            .Join(
                _context.Assessments,
                user => user.User_ID,
                assessment => assessment.Users.User_ID,
                (user, assessment) => new
                {
                    user.User_ID,
                    FullName = $"{user.User_FirstName} {user.User_LastName}",
                    user.User_Departmenr,
                    user.User_Designation,
                    assessment.Assessment_SelectedLevel
                }
            )
            .FirstOrDefaultAsync();
    }

    public async Task<List<string>> GetAssessmentTopicsByUserId(int userId)
    {
        var user = await _context.Users.Include(u => u.assessments).FirstOrDefaultAsync(u => u.User_ID == userId);

        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        var assessmentTopics = user.assessments?.Select(a => a.Assessment_SelectedTopic).ToList();

        if (assessmentTopics == null || assessmentTopics.Count == 0)
        {
            throw new KeyNotFoundException("No assessments found for the specified user ID.");
        }

        return assessmentTopics;
    }

    private bool AssessmentExists(int id)
    {
        return _context.Assessments.Any(e => e.Assessment_ID == id);
    }
}
