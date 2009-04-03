namespace ddd.belfast.ioc
{
    public interface IEmailSender
    {
        void Send(string from, string to, string subject, 
                                  string body, string[] fileAttachments);
    }

    public class EmailSender : IEmailSender
    {
        public void Send(string from, string to, string subject, 
                string body, string[] fileAttachments)
        {
            
        }
    }
}