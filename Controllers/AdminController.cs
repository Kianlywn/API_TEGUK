using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teguk_API.Data;

namespace Teguk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController
        : ControllerBase
    {
        private readonly
            AppDbContext
            _context;

        public AdminController(
            AppDbContext context)
        {
            _context =
                context;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            Dashboard()
        {
            var totalUsers =
                await _context
                .Accounts
                .CountAsync(x =>
                    x.Role ==
                    Models.Role.User);

            var totalExperts =
                await _context
                .Accounts
                .CountAsync(x =>
                    x.Role ==
                    Models.Role
                    .HealthExpert);

            var totalConsultations =
                await _context
                .Consultations
                .CountAsync();

            return Ok(new
            {
                totalUsers,
                totalExperts,
                totalConsultations
            });
        }

        [HttpGet("users")]
        public async Task<IActionResult>
            Users()
        {
            return Ok(
                await _context
                .Accounts
                .Where(x =>
                    x.Role ==
                    Models.Role
                    .User)
                .ToListAsync());
        }

        [HttpGet("experts")]
        public async Task<IActionResult>
            Experts()
        {
            return Ok(
                await _context
                .Accounts
                .Where(x =>
                    x.Role ==
                    Models.Role
                    .HealthExpert)
                .ToListAsync());
        }
    }
}