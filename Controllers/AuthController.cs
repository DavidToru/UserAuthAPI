using Microsoft.AspNetCore.Mvc;
using UserAuthAPI.models;
using UserAuthAPI.services;

namespace UserAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController()
        {
            _authService = new AuthService();
        }
        [HttpPost("signup")]
        public IActionResult Signup([FromBody] User request)
        {
            var result = _authService.Signup(request);
            return Ok(result);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var result = _authService.Login(request);
            return Ok(result);
        }
    }
}