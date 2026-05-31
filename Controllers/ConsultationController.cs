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
    public class ConsultationController
        : ControllerBase
    {
        private readonly
            IConsultationService
            _consultationService;

        public ConsultationController(
            IConsultationService
            consultationService)
        {
            _consultationService =
                consultationService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult>
            Create(
            CreateConsultationDto dto)
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                ClaimTypes
                .NameIdentifier)
                ?.Value);

            var result =
                await
                _consultationService
                .CreateConsultation(
                    userId,
                    dto);

            return Ok(result);
        }

        [HttpPost("message")]
        public async Task<IActionResult>
            SendMessage(
            SendMessageDto dto)
        {
            var senderId =
                Guid.Parse(
                User.FindFirst(
                ClaimTypes
                .NameIdentifier)
                ?.Value);

            var result =
                await
                _consultationService
                .SendMessage(
                    senderId,
                    dto);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetMessages(
            Guid id)
        {
            var result =
                await
                _consultationService
                .GetMessages(id);

            return Ok(result);
        }

        [HttpGet("my-consultations")]
        public async Task<IActionResult>
        MyConsultations()
            {
                var userId =
                    Guid.Parse(
                    User.FindFirst(
                        ClaimTypes
                        .NameIdentifier)
                    ?.Value);

                var role =
                    User.FindFirst(
                        ClaimTypes.Role)
                    ?.Value;

                var result =
                    await
                    _consultationService
                    .GetMyConsultations(
                        userId,
                        role);

                return Ok(result);
            }
    }
}