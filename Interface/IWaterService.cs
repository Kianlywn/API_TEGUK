using Teguk_API.DTOs;

namespace Teguk_API.Interfaces
{
    public interface IWaterService
    {
        Task<string>
            AddWater(
                Guid accountId,
                AddWaterDto dto);

        Task<object>
            GetTodayProgress(
                Guid accountId);

        Task<object>
            GetHistory(
                Guid accountId);
    }
}