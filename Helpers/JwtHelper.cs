using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Teguk_API.Models;

namespace Teguk_API.Helpers
{
    public class JwtHelper
    {
        public static string GenerateToken(
            IConfiguration configuration,
            Account account)
        {
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    account.Id.ToString()),

                new Claim(
                    ClaimTypes.Email,
                    account.Email),

                new Claim(
                    ClaimTypes.Role,
                    account.Role.ToString())
            };

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        configuration["Jwt:Key"]));

            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer:
                        configuration["Jwt:Issuer"],

                    audience:
                        configuration["Jwt:Audience"],

                    claims: claims,

                    expires:
                        DateTime.Now.AddDays(7),

                    signingCredentials:
                        creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}