using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teguk_API.Models
{
    public class Consultation
    {
        [Key]
        public Guid Id
        {
            get;
            set;
        }

        [ForeignKey("User")]
        public Guid UserId
        {
            get;
            set;
        }

        [ForeignKey("Expert")]
        public Guid ExpertId
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

        public Account User
        {
            get;
            set;
        }

        public Account Expert
        {
            get;
            set;
        }
    }
}