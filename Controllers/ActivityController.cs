using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teguk_API.DTOs;
using Teguk_API.Interfaces;

namespace Teguk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ActivityController
        : ControllerBase
    {
        private readonly
            IActivityService
            _activityService;

        public ActivityController(
            IActivityService
            activityService)
        {
            _activityService =
                activityService;
        }

        [HttpPost]
        public async Task<IActionResult>
            AddActivity(
            CreateActivityDto dto)
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                    ClaimTypes
                    .NameIdentifier)
                ?.Value);

            var result =
                await _activityService
                .AddActivity(
                    userId,
                    dto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult>
            GetActivities()
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                    ClaimTypes
                    .NameIdentifier)
                ?.Value);

            var result =
                await _activityService
                .GetActivities(
                    userId);

            return Ok(result);
        }
    }
}