namespace Teguk_API.DTOs
{
    public class UpdateProfileDto
    {
        public string FullName { get; set; }

        public int Age { get; set; }

        public double Weight { get; set; }

        public string Gender { get; set; }

        public string ActivityLevel { get; set; }

        public string EnvironmentCondition { get; set; }
    }
}