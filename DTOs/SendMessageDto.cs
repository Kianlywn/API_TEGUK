namespace Teguk_API.DTOs
{
    public class SendMessageDto
    {
        public Guid ConsultationId
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
}