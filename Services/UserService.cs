using Microsoft.EntityFrameworkCore;
using Teguk_API.Data;
using Teguk_API.DTOs;
using Teguk_API.Interfaces;

namespace Teguk_API.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<object>
            GetProfile(Guid accountId)
        {
            var profile =
                await _context.UserProfiles
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x =>
                    x.AccountId == accountId);

            if (profile == null)
            {
                return "Profile not found";
            }

            return new
            {
                id = profile.Account.Id,
                fullName =
                    profile.Account.FullName,

                email =
                    profile.Account.Email,

                age = profile.Age,
                weight = profile.Weight,
                gender = profile.Gender,

                activityLevel =
                    profile.ActivityLevel,

                environmentCondition =
                    profile
                    .EnvironmentCondition,

                targetWater =
                    profile.DailyWaterTarget
            };
        }

        public async Task<string>
            UpdateProfile(
            Guid accountId,
            UpdateProfileDto dto)
        {
            var profile =
                await _context.UserProfiles
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x =>
                    x.AccountId == accountId);

            if (profile == null)
            {
                return "Profile not found";
            }

            profile.Account.FullName =
                dto.FullName;

            profile.Age = dto.Age;
            profile.Weight = dto.Weight;
            profile.Gender = dto.Gender;

            profile.ActivityLevel =
                dto.ActivityLevel;

            profile.EnvironmentCondition =
                dto.EnvironmentCondition;

            // Smart water calculation
            int target =
                (int)(dto.Weight * 30);

            if (dto.ActivityLevel
                .ToLower() == "high")
            {
                target += 500;
            }

            if (dto.EnvironmentCondition
                .ToLower() == "hot")
            {
                target += 300;
            }

            profile.DailyWaterTarget =
                target;

            await _context.SaveChangesAsync();

            return "Profile updated";
        }
    }
}