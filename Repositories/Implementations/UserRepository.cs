using SkillAssessment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillAssessment.Data;

namespace SkillAssessment.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve users", ex);
            }
        }

        public async Task<User> GetUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve user", ex);
            }
        }

        public async Task CreateUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create user", ex);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.User_ID))
                {
                    throw new KeyNotFoundException($"User with ID {user.User_ID} not found.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update user", ex);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete user", ex);
            }
        }

        public async Task<IEnumerable<User>> GetUsersByEmailAsync(string userEmail)
        {
            try
            {
                return await _context.Users.Where(u => u.User_Email == userEmail).Include(x=>x.results).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve users by email", ex);
            }
        }

        public async Task<int?> GetUserIdByEmailAsync(string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Email == userEmail);
            return user?.User_ID;
        }

        public async Task<IEnumerable<string>> GetDepartmentsAsync()
        {
            return await _context.Users.Select(user => user.User_Departmenr).Distinct().ToListAsync();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.User_ID == id);
        }
    }
}
