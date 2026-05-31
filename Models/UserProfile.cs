using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Teguk_API.Models;

namespace Teguk_API.Models
{
    public class UserProfile
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        public int Age { get; set; }

        public double Weight { get; set; }

        public string Gender { get; set; }

        public string ActivityLevel { get; set; }

        public string EnvironmentCondition { get; set; }

        public int DailyWaterTarget { get; set; }

        public Account Account { get; set; }
    }
}