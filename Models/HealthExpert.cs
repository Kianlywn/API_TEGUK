using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teguk_API.Models
{
    public class HealthExpert
    {
        [Key]
        public Guid Id
        {
            get;
            set;
        }

        [ForeignKey("Account")]
        public Guid AccountId
        {
            get;
            set;
        }

        public string Profession
        {
            get;
            set;
        }

        public string Specialization
        {
            get;
            set;
        }

        public string LicenseNumber
        {
            get;
            set;
        }

        public int ExperienceYears
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        } = "Pending";

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