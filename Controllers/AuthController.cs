using Microsoft.AspNetCore.Mvc;
using Teguk_API.DTOs;
using Teguk_API.Interfaces;

namespace Teguk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController
        : ControllerBase
    {
        private readonly IAuthService
            _authService;

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult>
            Register(RegisterDto dto)
        {
            var result =
                await _authService
                .Register(dto);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult>
            Login(LoginDto dto)
        {
            var result =
                await _authService
                .Login(dto);

            return Ok(result);
        }
    }
}