namespace Teguk_API.DTOs
{
    public class RegisterDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public double Weight { get; set; }

        public string Gender { get; set; }

        public string ActivityLevel { get; set; }

        public string EnvironmentCondition { get; set; }

        public string Role { get; set; }
    }
}