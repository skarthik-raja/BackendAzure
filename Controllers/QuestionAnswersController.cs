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
    public class QuestionAnswersController : ControllerBase
    {
        private readonly IQuestionAnswerRepository _repository;

        public QuestionAnswersController(IQuestionAnswerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<IEnumerable<QuestionAnswer>>> GetQuestionAnswers()
        {
            var questionAnswers = await _repository.GetAllQuestionAnswersAsync();
            return Ok(questionAnswers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<QuestionAnswer>> GetQuestionAnswer(int id)
        {
            var questionAnswer = await _repository.GetQuestionAnswerByIdAsync(id);

            if (questionAnswer == null)
            {
                return NotFound();
            }

            return Ok(questionAnswer);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<IActionResult> PutQuestionAnswer(int id, QuestionAnswer updatedQuestionAnswer)
        {
            if (id != updatedQuestionAnswer.Id)
            {
                return BadRequest();
            }

            var result = await _repository.UpdateQuestionAnswerAsync(updatedQuestionAnswer);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<QuestionAnswer>> PostQuestionAnswer(QuestionAnswer questionAnswer)
        {
            var createdQuestionAnswer = await _repository.CreateQuestionAnswerAsync(questionAnswer);
            return CreatedAtAction("GetQuestionAnswer", new { id = createdQuestionAnswer.Id }, createdQuestionAnswer);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 400)]
        public async Task<IActionResult> DeleteQuestionAnswer(int id)
        {
            var result = await _repository.DeleteQuestionAnswerAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("Byresult/{result_id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<IEnumerable<QuestionAnswer>>> GetQuestionAnswersByresult(int result_id)
        {
            var questionAnswers = await _repository.GetQuestionAnswersByResultAsync(result_id);

            if (questionAnswers == null || !questionAnswers.Any())
            {
                return NotFound();
            }

            return Ok(questionAnswers);
        }
    }

}
