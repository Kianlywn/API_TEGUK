using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teguk_API.DTOs;
using Teguk_API.Interfaces;

namespace Teguk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController
        : ControllerBase
    {
        private readonly IUserService
            _userService;

        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        [Authorize(Roles = "User,HealthExpert")]
        public async Task<IActionResult>
            GetProfile()
        {
            var userId =
                User.FindFirst(
                ClaimTypes.NameIdentifier)
                ?.Value;

            var result =
                await _userService
                .GetProfile(
                    Guid.Parse(userId));

            return Ok(result);
        }

        [HttpPut("profile")]
        [Authorize(Roles = "User,HealthExpert")]
        public async Task<IActionResult>
            UpdateProfile(
            UpdateProfileDto dto)
        {
            var userId =
                User.FindFirst(
                ClaimTypes.NameIdentifier)
                ?.Value;

            var result =
                await _userService
                .UpdateProfile(
                    Guid.Parse(userId),
                    dto);

            return Ok(result);
        }

        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            return Ok(
                "Welcome Admin");
        }

        [HttpGet("expert-only")]
        [Authorize(
            Roles = "HealthExpert")]
        public IActionResult ExpertOnly()
        {
            return Ok(
                "Welcome Expert");
        }
    }
}