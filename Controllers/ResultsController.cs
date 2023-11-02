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
    public class ResultsController : ControllerBase
    {
        private readonly IResultRepository _repository;

        public ResultsController(IResultRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        [ProducesResponseType(typeof(IEnumerable<Result>), 200)]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            var results = await _repository.GetAllResultsAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            var result = await _repository.GetResultByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
            try
            {
                var createdResult = await _repository.CreateResultAsync(result);
                return CreatedAtAction("GetResult", new { id = createdResult.result_id }, createdResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 400)]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var deleteResult = await _repository.DeleteResultAsync(id);

            if (!deleteResult)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("GetResultsByUserId/{userId}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultsByUserId(int userId)
        {
            var results = await _repository.GetResultsByUserIdAsync(userId);

            if (results == null || !results.Any())
            {
                return NotFound("No results found for the specified user ID.");
            }

            return Ok(results);
        }

        [HttpGet("GetResultCountByUserId/{userId}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<int>> GetResultCountByUserId(int userId)
        {
            var resultCount = await _repository.GetResultCountByUserIdAsync(userId);
            return Ok(resultCount);
        }

        [HttpGet("GetTotalPointsByUserId/{userId}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<int>> GetTotalPointsByUserId(int userId)
        {
            var totalPoints = await _repository.GetTotalPointsByUserIdAsync(userId);
            return Ok(totalPoints);
        }
    }

}
