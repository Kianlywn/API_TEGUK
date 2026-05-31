using System.ComponentModel.DataAnnotations;
using Teguk_API.Models;

namespace Teguk_API.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public Role Role { get; set; }

        public bool IsVerified { get; set; } = true;

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}