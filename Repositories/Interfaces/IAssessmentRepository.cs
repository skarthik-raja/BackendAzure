using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkillAssessment.Models;

public interface IAssessmentRepository
{
    Task<IEnumerable<Assessment>> GetAllAssessments();
    Task<Assessment> GetAssessmentById(int assessmentId);
    Task<Assessment> CreateAssessment(Assessment assessment);
    Task<Assessment> UpdateAssessment(int assessmentId, string newAssessmentType);
    Task<IEnumerable<Assessment>> GetUnfinishedAssessmentsForUser(int userId);
    Task<int> GetNumberOfUnfinishedAssessmentsForUser(int userId);
    Task<Assessment> GetMaxAssessment(int userId);
    Task<object> GetAssessmentDetails(int userId);
    Task<List<string>> GetAssessmentTopicsByUserId(int userId);
}
