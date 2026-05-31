using Microsoft.EntityFrameworkCore;
using Teguk_API.Data;
using Teguk_API.DTOs;
using Teguk_API.Interfaces;
using Teguk_API.Models;

namespace Teguk_API.Services
{
    public class ReminderService
        : IReminderService
    {
        private readonly AppDbContext
            _context;

        public ReminderService(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<string>
            CreateReminder(
            Guid accountId,
            CreateReminderDto dto)
        {
            var reminder =
                new Reminder
                {
                    Id =
                        Guid.NewGuid(),

                    AccountId =
                        accountId,

                    ReminderTime =
                        TimeSpan.Parse(
                            dto.ReminderTime),

                    IntervalMinutes =
                        dto.IntervalMinutes
                };

            await _context.Reminders
                .AddAsync(reminder);

            await _context
                .SaveChangesAsync();

            return
                "Reminder created";
        }

        public async Task<object>
            GetReminders(
            Guid accountId)
        {
            return await _context
                .Reminders
                .Where(x =>
                    x.AccountId
                    == accountId)
                .ToListAsync();
        }

        public async Task<string>
            DeleteReminder(
            Guid reminderId,
            Guid accountId)
        {
            var reminder =
                await _context
                .Reminders
                .FirstOrDefaultAsync(x =>
                    x.Id
                    == reminderId
                    &&
                    x.AccountId
                    == accountId);

            if (reminder == null)
            {
                return
                    "Reminder not found";
            }

            _context.Reminders
                .Remove(reminder);

            await _context
                .SaveChangesAsync();

            return
                "Reminder deleted";
        }
    }
}