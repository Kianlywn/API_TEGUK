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
    public class WaterController
        : ControllerBase
    {
        private readonly IWaterService
            _waterService;

        public WaterController(
            IWaterService
            waterService)
        {
            _waterService =
                waterService;
        }

        [HttpPost]
        public async Task<IActionResult>
            AddWater(
            AddWaterDto dto)
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                    ClaimTypes
                    .NameIdentifier)
                ?.Value);

            var result =
                await _waterService
                .AddWater(
                    userId,
                    dto);

            return Ok(result);
        }

        [HttpGet("today")]
        public async Task<IActionResult>
            TodayProgress()
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                    ClaimTypes
                    .NameIdentifier)
                ?.Value);

            var result =
                await _waterService
                .GetTodayProgress(
                    userId);

            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult>
            History()
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                    ClaimTypes
                    .NameIdentifier)
                ?.Value);

            var result =
                await _waterService
                .GetHistory(
                    userId);

            return Ok(result);
        }
    }
}