using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Teguk_API.Data;

namespace Teguk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User,HealthExpert")]
    public class StatisticsController
        : ControllerBase
    {
        private readonly
            AppDbContext
            _context;

        public StatisticsController(
            AppDbContext context)
        {
            _context =
                context;
        }

        [HttpGet("weekly")]
        public async Task<IActionResult>
            Weekly()
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                    ClaimTypes
                    .NameIdentifier)
                ?.Value);

            var weekAgo =
                DateTime.UtcNow
                .AddDays(-7);

            var result =
                await _context
                .WaterIntakes
                .Where(x =>
                    x.AccountId
                    == userId
                    &&
                    x.DrinkTime
                    >= weekAgo)
                .GroupBy(x =>
                    x.DrinkTime.Date)
                .Select(x =>
                    new
                    {
                        date =
                            x.Key,

                        total =
                            x.Sum(y =>
                                y.AmountMl)
                    })
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("monthly")]
        public async Task<IActionResult>
            Monthly()
        {
            var userId =
                Guid.Parse(
                User.FindFirst(
                    ClaimTypes
                    .NameIdentifier)
                ?.Value);

            var monthAgo =
                DateTime.UtcNow
                .AddDays(-30);

            var result =
                await _context
                .WaterIntakes
                .Where(x =>
                    x.AccountId
                    == userId
                    &&
                    x.DrinkTime
                    >= monthAgo)
                .GroupBy(x =>
                    x.DrinkTime.Date)
                .Select(x =>
                    new
                    {
                        date =
                            x.Key,

                        total =
                            x.Sum(y =>
                                y.AmountMl)
                    })
                .ToListAsync();

            return Ok(result);
        }
    }
}