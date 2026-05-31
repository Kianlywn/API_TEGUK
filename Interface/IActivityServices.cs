using Teguk_API.DTOs;

namespace Teguk_API.Interfaces
{
    public interface IActivityService
    {
        Task<string>
            AddActivity(
                Guid accountId,
                CreateActivityDto dto);

        Task<object>
            GetActivities(
                Guid accountId);
    }
}