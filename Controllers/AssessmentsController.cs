using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillAssessment.Data;
using SkillAssessment.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentsController : ControllerBase
    {
        private readonly UserContext _context;

        private readonly IAssessmentRepository _assessmentRepository;

        public AssessmentsController(IAssessmentRepository assessmentRepository, UserContext context)
        {
            _assessmentRepository = assessmentRepository;
            _context = context;
        }

        // GET: api/Assessments
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessments()
        {
            return Ok(await _assessmentRepository.GetAllAssessments());
        }

        // POST: api/Assessments
        [HttpPost]
        
        public async Task<ActionResult<Assessment>> PostAssessment(Assessment assessment)
        {
            // Implementation of PostAssessment method
            return Ok(await _assessmentRepository.CreateAssessment(assessment));
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> PutAssessment(int id, string newAssessmentType)
        {
            // Implementation of PutAssessment method
            return Ok(await _assessmentRepository.UpdateAssessment(id, newAssessmentType));
        }

        // Implement other methods using the repository...

        // Custom method: GetAssessmentDetails
        [HttpGet("assessment-details/{userId}")]
        
        public async Task<ActionResult<object>> GetAssessmentDetails(int userId)
        {
            return Ok(await _assessmentRepository.GetAssessmentDetails(userId));
        }

        // Custom method: GetMaxAssessment
        [HttpGet("max-assessment/{userId}")]
        
        public async Task<ActionResult<Assessment>> GetMaxAssessment(int userId)
        {
            return Ok(await _assessmentRepository.GetMaxAssessment(userId));
        }

        // Custom method: GetAssessmentTopicsByUserId
        [HttpGet("assessment-topics/user/{id}")]
       
        public async Task<ActionResult<List<string>>> GetAssessmentTopicsByUserId(int id)
        {
            return Ok(await _assessmentRepository.GetAssessmentTopicsByUserId(id));
        }

        // Custom method: GetUnfinishedAssessmentsForUser
        [HttpGet("unfinished-assessments/{userId}")]
       
        public async Task<ActionResult<IEnumerable<Assessment>>> GetUnfinishedAssessmentsForUser(int userId)
        {
            return Ok(await _assessmentRepository.GetUnfinishedAssessmentsForUser(userId));
        }

        // Custom method: GetNumberOfUnfinishedAssessmentsForUser
        [HttpGet("unfinished-assessments/count/{userId}")]
       
        public async Task<ActionResult<int>> GetNumberOfUnfinishedAssessmentsForUser(int userId)
        {
            return Ok(await _assessmentRepository.GetNumberOfUnfinishedAssessmentsForUser(userId));
        }

        [HttpGet("assessment")]
       
        public async Task<ActionResult<Assessment>> GetAssessmentId(int assessmentId)
        {
            var assessment = await _context.Assessments
                .Include(a => a.Topics)
                .Include(a => a.Users)
                .FirstOrDefaultAsync(a => a.Assessment_ID == assessmentId);

            if (assessment == null)
            {
                return NotFound("Assessment not found.");
            }

            return assessment;
        }

        // GET: api/Assessments/recent-assessment
        [HttpGet("recent-assessment")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<Assessment>> GetRecentAssessment()
        {
            var recentAssessment = await _context.Assessments
                .OrderByDescending(a => a.Assessment_ID)
                .FirstOrDefaultAsync();

            if (recentAssessment == null)
            {
                return NotFound("No assessments found.");
            }

            return recentAssessment;
        }

    }
}
