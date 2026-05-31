using Teguk_API.DTOs;

namespace Teguk_API.Interfaces
{
    public interface IHealthExpertService
    {
        Task<string>
            ApplyExpert(
                Guid userId,
                ApplyExpertDto dto);

        Task<object>
            GetMyApplication(
                Guid userId);

        Task<object>
            GetPendingExperts();

        Task<object>
            GetExpertList();

        Task<string>
            ApproveExpert(
                Guid expertId);

        Task<string>
            RejectExpert(
                Guid expertId);
    }
}