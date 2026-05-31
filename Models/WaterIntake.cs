using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teguk_API.Models
{
    public class WaterIntake
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        public int AmountMl { get; set; }

        public DateTime DrinkTime
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