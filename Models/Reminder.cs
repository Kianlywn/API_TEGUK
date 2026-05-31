using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teguk_API.Models
{
    public class Reminder
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId
        {
            get;
            set;
        }

        public TimeSpan ReminderTime
        {
            get;
            set;
        }

        public int IntervalMinutes
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        } = true;

        public Account Account
        {
            get;
            set;
        }
    }
}