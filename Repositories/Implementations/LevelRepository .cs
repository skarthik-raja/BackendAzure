using Microsoft.EntityFrameworkCore;
using SkillAssessment.Data;
using SkillAssessment.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillAssessment.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly UserContext _context;

        public LevelRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Level>> GetLevelsAsync()
        {
            return await _context.levels.ToListAsync();
        }

        public async Task<Level> GetLevelByIdAsync(int id)
        {
            return await _context.levels.FindAsync(id);
        }

        public async Task<Level> AddLevelAsync(Level level)
        {
            _context.levels.Add(level);
            await _context.SaveChangesAsync();
            return level;
        }

        public async Task<bool> UpdateLevelAsync(Level level)
        {
            _context.Entry(level).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteLevelAsync(int id)
        {
            var level = await _context.levels.FindAsync(id);

            if (level == null)
                return false;

            _context.levels.Remove(level);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
