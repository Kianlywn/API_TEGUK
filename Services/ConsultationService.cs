using Microsoft.EntityFrameworkCore;
using Teguk_API.Data;
using Teguk_API.DTOs;
using Teguk_API.Interfaces;
using Teguk_API.Models;

namespace Teguk_API.Services
{
    public class ConsultationService
        : IConsultationService
    {
        private readonly AppDbContext
            _context;

        public ConsultationService(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<string>
            CreateConsultation(
            Guid userId,
            CreateConsultationDto dto)
        {
            var consultation =
                new Consultation
                {
                    Id =
                        Guid.NewGuid(),

                    UserId =
                        userId,

                    ExpertId =
                        dto.ExpertId
                };

            await _context
                .Consultations
                .AddAsync(
                    consultation);

            await _context
                .SaveChangesAsync();

            return
                "Consultation created";
        }

        public async Task<string>
            SendMessage(
            Guid senderId,
            SendMessageDto dto)
        {
            var chat =
                new ConsultationMessage
                {
                    Id =
                        Guid.NewGuid(),

                    ConsultationId =
                        dto
                        .ConsultationId,

                    SenderId =
                        senderId,

                    Message =
                        dto.Message
                };

            await _context
                .ConsultationMessages
                .AddAsync(chat);

            await _context
                .SaveChangesAsync();

            return
                "Message sent";
        }

        public async Task<object>
            GetMessages(
            Guid consultationId)
        {
            return await _context
                .ConsultationMessages
                .Include(x =>
                    x.Sender)
                .Where(x =>
                    x.ConsultationId
                    == consultationId)
                .OrderBy(x =>
                    x.SentAt)
                .Select(x =>
                    new
                    {
                        sender =
                            x.Sender
                            .FullName,

                        message =
                            x.Message,

                        sentAt =
                            x.SentAt
                    })
                .ToListAsync();
        }

        public async Task<object>
        GetMyConsultations(
        Guid accountId)
        {
            return await _context
                .Consultations
                .Include(x => x.Expert)
                .Where(x =>
                    x.UserId
                    == accountId)
                .Select(x =>
                    new
                    {
                        consultationId =
                            x.Id,

                        expertName =
                            x.Expert
                            .FullName,

                        status =
                            x.Status,

                        createdAt =
                            x.CreatedAt
                    })
                .ToListAsync();
        }

        public async Task<object>
        GetIncomingConsultations(
        Guid expertId)
        {
            return await _context
                .Consultations
                .Include(x => x.User)
                .Where(x =>
                    x.ExpertId
                    == expertId)
                .Select(x =>
                    new
                    {
                        consultationId =
                            x.Id,

                        userName =
                            x.User
                            .FullName,

                        status =
                            x.Status,

                        createdAt =
                            x.CreatedAt
                    })
                .ToListAsync();
        }
    }
}