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
    public class HealthExpertController
        : ControllerBase
    {
        private readonly
            IHealthExpertService
            _expertService;

        public HealthExpertController(
            IHealthExpertService
            expertService)
        {
            _expertService =
                expertService;
        }

        [HttpPost("apply")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult>
            Apply(
            ApplyExpertDto dto)
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                    ClaimTypes
                    .NameIdentifier)
                ?.Value);

            var result =
                await _expertService
                .ApplyExpert(
                    userId,
                    dto);

            return Ok(result);
        }

        [HttpGet("my-application")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult>
            MyApplication()
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                    ClaimTypes
                    .NameIdentifier)
                ?.Value);

            var result =
                await _expertService
                .GetMyApplication(
                    userId);

            return Ok(result);
        }

        [HttpGet("pending")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>
            Pending()
        {
            var result =
                await _expertService
                .GetPendingExperts();

            return Ok(result);
        }

        [HttpPut("approve/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>
            Approve(Guid id)
        {
            var result =
                await _expertService
                .ApproveExpert(id);

            return Ok(result);
        }

        [HttpPut("reject/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>
            Reject(Guid id)
        {
            var result =
                await _expertService
                .RejectExpert(id);

            return Ok(result);
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult>
    ExpertList()
        {
            var result =
                await _expertService
                .GetExpertList();

            return Ok(result);
        }
    }
}