namespace Teguk_API.DTOs
{
    public class CreateReminderDto
    {
        public string ReminderTime
        {
            get;
            set;
        }

        public int IntervalMinutes
        {
            get;
            set;
        }
    }
}