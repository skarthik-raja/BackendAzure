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
    public class LevelsController : ControllerBase
    {
        private readonly ILevelRepository _levelRepository;

        public LevelsController(ILevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        // GET: api/Levels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Level>>> GetLevels()
        {
            var levels = await _levelRepository.GetLevelsAsync();
            return Ok(levels);
        }

        // GET: api/Levels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Level>> GetLevel(int id)
        {
            var level = await _levelRepository.GetLevelByIdAsync(id);

            if (level == null)
            {
                return NotFound();
            }

            return Ok(level);
        }

        // POST: api/Levels
        [HttpPost]
        public async Task<ActionResult<Level>> PostLevel(Level level)
        {
            var addedLevel = await _levelRepository.AddLevelAsync(level);
            return CreatedAtAction(nameof(GetLevel), new { id = addedLevel.LevelId }, addedLevel);
        }

        // PUT: api/Levels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLevel(int id, Level level)
        {
            if (id != level.LevelId)
            {
                return BadRequest();
            }

            var updated = await _levelRepository.UpdateLevelAsync(level);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Levels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLevel(int id)
        {
            var deleted = await _levelRepository.DeleteLevelAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
