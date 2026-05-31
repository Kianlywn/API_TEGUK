using Teguk_API.DTOs;

namespace Teguk_API.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto dto);

        Task<object> Login(LoginDto dto);
    }
}