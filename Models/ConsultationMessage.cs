using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teguk_API.Models
{
    public class ConsultationMessage
    {
        [Key]
        public Guid Id
        {
            get;
            set;
        }

        [ForeignKey("Consultation")]
        public Guid ConsultationId
        {
            get;
            set;
        }

        [ForeignKey("Sender")]
        public Guid SenderId
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public DateTime SentAt
        {
            get;
            set;
        } = DateTime.UtcNow;

        public Consultation Consultation
        {
            get;
            set;
        }

        public Account Sender
        {
            get;
            set;
        }
    }
}