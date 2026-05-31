using Microsoft.EntityFrameworkCore;
using Teguk_API.Data;
using Teguk_API.DTOs;
using Teguk_API.Interfaces;
using Teguk_API.Models;

namespace Teguk_API.Services
{
    public class HealthExpertService
        : IHealthExpertService
    {
        private readonly AppDbContext
            _context;

        public HealthExpertService(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<string>
            ApplyExpert(
            Guid userId,
            ApplyExpertDto dto)
        {
            var exists =
                await _context
                .HealthExperts
                .AnyAsync(x =>
                    x.AccountId
                    == userId);

            if (exists)
            {
                return
                    "Application already exists";
            }

            var expert =
                new HealthExpert
                {
                    Id =
                        Guid.NewGuid(),

                    AccountId =
                        userId,

                    Profession =
                        dto.Profession,

                    Specialization =
                        dto.Specialization,

                    LicenseNumber =
                        dto.LicenseNumber,

                    ExperienceYears =
                        dto.ExperienceYears,

                    Status =
                        "Pending"
                };

            await _context
                .HealthExperts
                .AddAsync(expert);

            await _context
                .SaveChangesAsync();

            return
                "Application submitted";
        }

        public async Task<object>
            GetMyApplication(
            Guid userId)
        {
            return await _context
                .HealthExperts
                .FirstOrDefaultAsync(x =>
                    x.AccountId
                    == userId);
        }

        public async Task<object>
            GetPendingExperts()
        {
            return await _context
                .HealthExperts
                .Include(x =>
                    x.Account)
                .Where(x =>
                    x.Status
                    == "Pending")
                .Select(x =>
                    new
                    {
                        x.Id,
                        x.Profession,
                        x.Specialization,
                        x.ExperienceYears,
                        fullname =
                            x.Account
                            .FullName,
                        email =
                            x.Account
                            .Email
                    })
                .ToListAsync();
        }

        public async Task<string>
            ApproveExpert(
            Guid expertId)
        {
            var expert =
                await _context
                .HealthExperts
                .Include(x =>
                    x.Account)
                .FirstOrDefaultAsync(x =>
                    x.Id
                    == expertId);

            if (expert == null)
            {
                return
                    "Application not found";
            }

            expert.Status =
                "Approved";

            expert.Account.Role =
                Role.HealthExpert;

            await _context
                .SaveChangesAsync();

            return
                "Expert approved";
        }

        public async Task<string>
            RejectExpert(
            Guid expertId)
        {
            var expert =
                await _context
                .HealthExperts
                .FirstOrDefaultAsync(x =>
                    x.Id
                    == expertId);

            if (expert == null)
            {
                return
                    "Application not found";
            }

            expert.Status =
                "Rejected";

            await _context
                .SaveChangesAsync();

            return
                "Expert rejected";
        }

        public async Task<object>
    GetExpertList()
        {
            return await _context
                .HealthExperts
                .Include(x => x.Account)
                .Where(x =>
                    x.Status
                    == "Approved")
                .Select(x =>
                    new
                    {
                        expertId =
                            x.AccountId,

                        fullName =
                            x.Account
                            .FullName,

                        profession =
                            x.Profession,

                        specialization =
                            x.Specialization,

                        experienceYears =
                            x.ExperienceYears
                    })
                .ToListAsync();
        }
    }
}