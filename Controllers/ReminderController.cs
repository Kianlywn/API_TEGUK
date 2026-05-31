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
    public class ReminderController
        : ControllerBase
    {
        private readonly
            IReminderService
            _reminderService;

        public ReminderController(
            IReminderService
            reminderService)
        {
            _reminderService =
                reminderService;
        }

        [HttpPost]
        public async Task<IActionResult>
            CreateReminder(
            CreateReminderDto dto)
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                ClaimTypes
                .NameIdentifier)
                ?.Value);

            var result =
                await _reminderService
                .CreateReminder(
                    userId,
                    dto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult>
            GetReminders()
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                ClaimTypes
                .NameIdentifier)
                ?.Value);

            var result =
                await _reminderService
                .GetReminders(
                    userId);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            DeleteReminder(
            Guid id)
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                ClaimTypes
                .NameIdentifier)
                ?.Value);

            var result =
                await _reminderService
                .DeleteReminder(
                    id,
                    userId);

            return Ok(result);
        }
    }
}