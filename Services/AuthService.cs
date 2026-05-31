using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Teguk_API.Data;
using Teguk_API.DTOs;
using Teguk_API.Helpers;
using Teguk_API.Interfaces;
using Teguk_API.Models;

namespace Teguk_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(
            AppDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string>
            Register(RegisterDto dto)
        {
            var emailExists =
                await _context.Accounts
                .AnyAsync(x =>
                    x.Email == dto.Email);

            if (emailExists)
            {
                return "Email already exists";
            }

            Enum.TryParse<Role>(
                dto.Role,
                true,
                out var role);

            var account =
                new Account
                {
                    Id = Guid.NewGuid(),
                    FullName = dto.FullName,
                    Email = dto.Email,
                    PasswordHash =
                        BCrypt.Net.BCrypt.HashPassword(
                            dto.Password),
                    Role = role
                };

            await _context.Accounts
                .AddAsync(account);

            await _context.SaveChangesAsync();

            if (role == Role.User)
            {
                var profile =
                    new UserProfile
                    {
                        Id = Guid.NewGuid(),
                        AccountId = account.Id,
                        Age = dto.Age,
                        Weight = dto.Weight,
                        Gender = dto.Gender,
                        ActivityLevel =
                            dto.ActivityLevel,

                        EnvironmentCondition =
                            dto.EnvironmentCondition
                    };

                await _context.UserProfiles
                    .AddAsync(profile);
            }

            await _context.SaveChangesAsync();

            return "Register Success";
        }

        public async Task<object>
            Login(LoginDto dto)
        {
            var account =
                await _context.Accounts
                .FirstOrDefaultAsync(x =>
                    x.Email == dto.Email);

            if (account == null)
            {
                return "Email not found";
            }

            bool verifyPassword =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    account.PasswordHash);

            if (!verifyPassword)
            {
                return "Wrong password";
            }

            var token =
                JwtHelper.GenerateToken(
                    _configuration,
                    account);

            return new
            {
                token,
                role =
                    account.Role.ToString(),

                email =
                    account.Email,

                fullname =
                    account.FullName
            };
        }
    }
}