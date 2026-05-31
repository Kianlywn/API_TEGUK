using Microsoft.EntityFrameworkCore;
using Teguk_API.Data;
using Teguk_API.DTOs;
using Teguk_API.Interfaces;
using Teguk_API.Models;

namespace Teguk_API.Services
{
    public class ActivityService
        : IActivityService
    {
        private readonly AppDbContext
            _context;

        public ActivityService(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<string>
            AddActivity(
            Guid accountId,
            CreateActivityDto dto)
        {
            var activity =
                new ActivityTracking
                {
                    Id =
                        Guid.NewGuid(),

                    AccountId =
                        accountId,

                    ActivityType =
                        dto.ActivityType,

                    ActivityLevel =
                        dto.ActivityLevel
                };

            await _context
                .ActivityTrackings
                .AddAsync(activity);

            await _context
                .SaveChangesAsync();

            return
                "Activity added";
        }

        public async Task<object>
            GetActivities(
            Guid accountId)
        {
            return await _context
                .ActivityTrackings
                .Where(x =>
                    x.AccountId
                    == accountId)
                .OrderByDescending(x =>
                    x.CreatedAt)
                .ToListAsync();
        }
    }
}