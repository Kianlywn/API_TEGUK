using Teguk_API.DTOs;

namespace Teguk_API.Interfaces
{
    public interface IUserService
    {
        Task<object> GetProfile(Guid accountId);

        Task<string> UpdateProfile(
            Guid accountId,
            UpdateProfileDto dto);
    }
}