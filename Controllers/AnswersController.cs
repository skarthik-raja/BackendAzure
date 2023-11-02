using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillAssessment.Models;
using SkillAssessment.Repositories;

namespace SkillAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswersController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            var answers = await _answerRepository.GetAllAnswersAsync();
            return Ok(answers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<Answer>> GetAnswer(int id)
        {
            var answer = await _answerRepository.GetAnswerByIdAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<IActionResult> PutAnswer(int id, Answer answer)
        {
            var result = await _answerRepository.UpdateAnswerAsync(id, answer);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            var createdAnswer = await _answerRepository.CreateAnswerAsync(answer);
            return CreatedAtAction("GetAnswer", new { id = createdAnswer.Id }, createdAnswer);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 400)]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var result = await _answerRepository.DeleteAnswerAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
