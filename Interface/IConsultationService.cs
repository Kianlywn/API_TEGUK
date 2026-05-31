using Teguk_API.DTOs;

namespace Teguk_API.Interfaces
{
    public interface IConsultationService
    {
        Task<string>
            CreateConsultation(
                Guid userId,
                CreateConsultationDto dto);

        Task<string>
            SendMessage(
                Guid senderId,
                SendMessageDto dto);

        Task<object>
            GetMessages(
                Guid consultationId);

        Task<object>
           GetMyConsultations(
               Guid accountId,
               string role);
    }
}