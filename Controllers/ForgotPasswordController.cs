using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using SkillAssessment.Data;
using SkillAssessment.Models;
using System.Text;

namespace SkillAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly UserContext _context;

        public ForgotPasswordController(UserContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            string email = request.Email;

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Please provide a valid email address.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Email == email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            string randomPassword = GenerateRandomPassword();

            user.User_Password = randomPassword;
            await _context.SaveChangesAsync();

            return Ok("Password reset successful. Check your email for the new password." + randomPassword);
        }

        private string GenerateRandomPassword()
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+";
            int passwordLength = 10;

            Random random = new Random();
            var password = new StringBuilder(passwordLength);
            for (int i = 0; i < passwordLength; i++)
            {
                password.Append(allowedChars[random.Next(allowedChars.Length)]);
            }

            return password.ToString();
        }
    }

    public class ForgotPasswordRequest
    {
        public string? Email { get; set; }
    }
}

