using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teguk_API.Models
{
    public class ActivityTracking
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId
        {
            get;
            set;
        }

        public string ActivityType
        {
            get;
            set;
        }

        public string ActivityLevel
        {
            get;
            set;
        }

        public DateTime CreatedAt
        {
            get;
            set;
        } = DateTime.UtcNow;

        public Account Account
        {
            get;
            set;
        }
    }
}