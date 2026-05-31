using Microsoft.EntityFrameworkCore;
using Teguk_API.Data;
using Teguk_API.DTOs;
using Teguk_API.Interfaces;
using Teguk_API.Models;

namespace Teguk_API.Services
{
    public class WaterService
        : IWaterService
    {
        private readonly AppDbContext
            _context;

        public WaterService(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<string>
            AddWater(
            Guid accountId,
            AddWaterDto dto)
        {
            var water =
                new WaterIntake
                {
                    Id =
                        Guid.NewGuid(),

                    AccountId =
                        accountId,

                    AmountMl =
                        dto.AmountMl
                };

            await _context
                .WaterIntakes
                .AddAsync(water);

            await _context
                .SaveChangesAsync();

            return
                "Water intake added";
        }

        public async Task<object>
        GetTodayProgress(
        Guid accountId)
            {
                var today =
                    DateTime.UtcNow.Date;

                var total =
                    await _context
                    .WaterIntakes
                    .Where(x =>
                        x.AccountId == accountId
                        &&
                        x.DrinkTime.Date
                        == today)
                    .SumAsync(x =>
                        x.AmountMl);

                var profile =
                    await _context
                    .UserProfiles
                    .FirstOrDefaultAsync(x =>
                        x.AccountId
                        == accountId);

                int target =
                    profile?.DailyWaterTarget ?? 0;

                // Prevent divide by zero
                if (target <= 0)
                {
                    target = 2000;
            }

            double percentage =
                total > 0
                ? (double)total / target * 100
                : 0;

            return new
            {
                totalDrink = total,
                target,
                percentage =
                    Math.Round(
                        percentage,
                        2)
            };
        }

        public async Task<object>
            GetHistory(
            Guid accountId)
        {
            return await _context
                .WaterIntakes
                .Where(x =>
                    x.AccountId
                    == accountId)
                .OrderByDescending(x =>
                    x.DrinkTime)
                .ToListAsync();
        }
    }
}