using Teguk_API.DTOs;

namespace Teguk_API.Interfaces
{
    public interface IReminderService
    {
        Task<string>
            CreateReminder(
                Guid accountId,
                CreateReminderDto dto);

        Task<object>
            GetReminders(
                Guid accountId);

        Task<string>
            DeleteReminder(
                Guid reminderId,
                Guid accountId);
    }
}