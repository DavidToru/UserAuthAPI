using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Tracing;

namespace UserAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private static List<User> users = new List<User>();
        [HttpPost ("signup")]
        public IActionResult signup([FromBody] User request)
        {
            bool emailExists = users.Any(u => u.Email == request.Email);
            if (emailExists)
            {
                return BadRequest("Email already Exists ");

            }
            User newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            users.Add(newUser);
            return Ok("signup successful");
        }


        [HttpPost ("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            User existingUser = users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser == null)
            {
                return NotFound("user not found");

            }
            bool PasswordMatch = BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password);
            if (!PasswordMatch)
            {
                return BadRequest("Wrong Password");

            }
            return Ok(new
            {
                existingUser.FirstName,
                existingUser.LastName,

                existingUser.Email


            });

        }

    }
}
