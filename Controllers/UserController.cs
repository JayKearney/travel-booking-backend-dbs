using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;

namespace TravelBooking_Backend_Jesica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        // Hardcoded credentials for demo purposes
        private const string DEMO_USERNAME = "testuser";
        private const string DEMO_PASSWORD = "password123"; // Change this to your preferred password

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                // Simple credential check with hardcoded values
                if (loginRequest.Username == DEMO_USERNAME && loginRequest.Password == DEMO_PASSWORD)
                {
                    _logger.LogInformation($"Login successful for user: {loginRequest.Username}");
                    return Ok(new { message = "Login successful!" });
                }
                else
                {
                    _logger.LogWarning($"Login failed for user: {loginRequest.Username}");
                    return Unauthorized("Invalid credentials");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during login: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // We can keep this endpoint but make it return a message that registration is disabled
        [HttpPost("Register")]
        public IActionResult Register([FromBody] LoginRequest loginRequest)
        {
            return Ok("Registration is not required. Use the demo account to log in.");
        }
    }

    public class LoginRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
}