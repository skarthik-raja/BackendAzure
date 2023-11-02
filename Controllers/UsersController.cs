using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillAssessment.Data;
using SkillAssessment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserContext _context;

        public UsersController(IUserRepository userRepository,UserContext userContext)
        {
            _userRepository = userRepository;
            _context = userContext;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve users",
                    Detail = ex.Message
                });
            }
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(id);

                return user;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "User not found",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve user",
                    Detail = ex.Message
                });
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            try
            {
                if (id != user.User_ID)
                {
                    return BadRequest();
                }

                await _userRepository.UpdateUserAsync(user);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "User not found",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to update user",
                    Detail = ex.Message
                });
            }
        }

        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<User>> PostUser([FromForm] User user, IFormFile imageFile)
        {
            try
            {
                // Check if an image file was uploaded
                if (imageFile != null)
                {
                    // Convert the image file to Base64 string and store it in the User_Image property
                    using (var ms = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(ms);
                        user.User_Image = Convert.ToBase64String(ms.ToArray());
                    }
                }

                // Save the user to the database
                await _userRepository.CreateUserAsync(user);

                return CreatedAtAction("GetUser", new { id = user.User_ID }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to create user",
                    Detail = ex.Message
                });
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 400)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "User not found",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to delete user",
                    Detail = ex.Message
                });
            }
        }

        // GET: api/Users/GetByEmail
        [HttpGet("GetByEmail")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByEmail(string userEmail)
        {
            try
            {
                var users = await _userRepository.GetUsersByEmailAsync(userEmail);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve users by email",
                    Detail = ex.Message
                });
            }
        }

        [HttpGet("GetByEmailID")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<int?> GetUserIdByEmailAsync(string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Email == userEmail);
            return user?.User_ID;
        }

        // Modify the API method to implement the DTO inline
        [HttpGet("GetUnmatchedUserByEmail")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<IEnumerable<object>>> GetUnmatchedUsersByEmail(string userEmail)
        {
            try
            {
                // Retrieve the user with the provided email
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.User_Email == userEmail);

                if (user == null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "User not found",
                        Detail = "No user found with the specified email."
                    });
                }

                // Retrieve all unmatched users based on the provided email
                var unmatchedUsers = await _context.Users
                    .Where(u => u.User_Email != userEmail)
                    .Include(x => x.results)
                    .ToListAsync();

                // Create a list to store the user data with total points
                var unmatchedUsersWithTotalPoints = new List<object>();

                foreach (var unmatchedUser in unmatchedUsers)
                {
                    // Calculate the total points for the user by summing up the points from all results
                    int totalPoints = unmatchedUser.results.Sum(r => r.points);

                    // Create an anonymous object with user data and total points
                    var userWithTotalPoints = new
                    {
                        unmatchedUser.User_ID,
                        unmatchedUser.User_FirstName,
                        unmatchedUser.User_LastName,
                        unmatchedUser.User_Address,
                        unmatchedUser.User_Departmenr,
                        unmatchedUser.User_Designation,
                        unmatchedUser.User_Email,
                        unmatchedUser.User_DOB,
                        unmatchedUser.User_EduLevel,
                        unmatchedUser.User_Gender,
                        TotalPoints = totalPoints,
                        unmatchedUser.assessments,
                        unmatchedUser.results
                        
                    };

                    // Add the anonymous object to the list
                    unmatchedUsersWithTotalPoints.Add(userWithTotalPoints);
                }

                // Return the list of anonymous objects containing user data with total points
                return Ok(unmatchedUsersWithTotalPoints);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve unmatched users by email",
                    Detail = ex.Message
                });
            }
        }

        // GET: api/Users/GetUsersByEmailWithResultCount
        [HttpGet("GetUsersByEmailWithResultCount")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<IEnumerable<object>>> GetUsersByEmailWithResultCount(string userEmail)
        {
            try
            {
                var usersWithResults = await _context.Users
                    .Where(u => u.User_Email == userEmail)
                    .Include(u => u.results)
                    .ToListAsync();

                if (!usersWithResults.Any())
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "User not found",
                        Detail = "No user found with the specified email."
                    });
                }

                var usersWithResultCount = new List<object>();

                foreach (var user in usersWithResults)
                {
                    int resultCount = user.results.Count;

                    var resultsData = await _context.Results
                        .Where(r => r.users.User_ID == user.User_ID)
                        .ToListAsync();

                    var userWithResultCount = new
                    {
                        user.User_ID,
                        user.User_FirstName,
                        user.User_LastName,
                        ResultCount = resultCount,
                        ResultsData = resultsData 
                    };

                    usersWithResultCount.Add(userWithResultCount);
                }

                return Ok(usersWithResultCount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve users by email with result count",
                    Detail = ex.Message
                });
            }
        }

        [HttpGet("GetDepartments")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult<IEnumerable<string>>> GetDepartments()
        {
            try
            {
                var departments = await _userRepository.GetDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve departments",
                    Detail = ex.Message
                });
            }
        }

        [HttpPut("UpdateUserExceptPasswordAndImage/{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<IActionResult> UpdateUserExceptPasswordAndImage(int id, [FromBody] User updatedUserData)
        {
            try
            {
                var existingUser = await _userRepository.GetUserAsync(id);

                if (existingUser == null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "User not found",
                        Detail = "No user found with the specified ID."
                    });
                }

                // Exclude the password and image fields from the update
                updatedUserData.User_Password = existingUser.User_Password;
                updatedUserData.User_Image = existingUser.User_Image;

                // Update other fields
                existingUser.User_FirstName = updatedUserData.User_FirstName;
                existingUser.User_LastName = updatedUserData.User_LastName;
                existingUser.User_Departmenr = updatedUserData.User_Departmenr;
                existingUser.User_Designation = updatedUserData.User_Designation;
                existingUser.User_DOB = updatedUserData.User_DOB;
                existingUser.User_Gender = updatedUserData.User_Gender;
                existingUser.User_EduLevel = updatedUserData.User_EduLevel;
                existingUser.User_PhoneNo = updatedUserData.User_PhoneNo;
                existingUser.User_Location = updatedUserData.User_Location;
                existingUser.User_Address = updatedUserData.User_Address;
                existingUser.User_Email = updatedUserData.User_Email;

                await _userRepository.UpdateUserAsync(existingUser);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "User not found",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to update user",
                    Detail = ex.Message
                });
            }
        }


        // PUT: api/Users/UpdatePassword/{id}
        [HttpPut("UpdatePassword/{id}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] string newPassword)
        {
            try
            {
                var existingUser = await _userRepository.GetUserAsync(id);

                if (existingUser == null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "User not found",
                        Detail = "No user found with the specified ID."
                    });
                }

                // Update the password field
                existingUser.User_Password = newPassword;

                await _userRepository.UpdateUserAsync(existingUser);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "User not found",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to update password",
                    Detail = ex.Message
                });
            }
        }
    }
}
