using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillAssessment.Models;

namespace SkillAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _repository;

        public QuestionsController(IQuestionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions()
        {
            var questions = await _repository.GetAllQuestionsAsync();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<Questions>> GetQuestion(int id)
        {
            var question = await _repository.GetQuestionByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<IActionResult> PutQuestion(int id, Questions question)
        {
            if (id != question.QnId)
            {
                return BadRequest();
            }

            var result = await _repository.UpdateQuestionAsync(question);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<Questions>> PostQuestion(Questions question)
        {
            var createdQuestion = await _repository.CreateQuestionAsync(question);
            return CreatedAtAction("GetQuestion", new { id = createdQuestion.QnId }, createdQuestion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var result = await _repository.DeleteQuestionAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("topicid")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<IActionResult> GetQuestionsByTopic(int topicId)
        {
            var questions = await _repository.GetQuestionsByTopicAsync(topicId);

            if (questions == null || !questions.Any())
            {
                return NotFound();
            }

            return Ok(questions);
        }

        [HttpGet("bytopicandlevel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuestionsByTopicAndLevel(int topicId, int levelId)
        {
            var questions = await _repository.GetQuestionsByTopicAndLevelAsync(topicId, levelId);

            if (questions == null || !questions.Any())
            {
                return NotFound();
            }

            return Ok(questions);
        }

    }

}
