namespace ddd.belfast.ioc
{
    public interface IEmailSender
    {
        void Send(string from, string to, string subject, 
                                  string body, string[] fileAttachments);
    }

    public class EmailSender : IEmailSender
    {
        private readonly IEmailTemplatingEngine _emailTemplatingEngine;

        public void Send(string from, string to, string subject, 
                string body, string[] fileAttachments)
        {
            
        }

        public EmailSender(IEmailTemplatingEngine emailTemplatingEngine)
        {
            _emailTemplatingEngine = emailTemplatingEngine;
        }
    }

    public interface IEmailTemplatingEngine
    {
    }

    public class EmailTemplatingEngine : IEmailTemplatingEngine
    {
        
    }
}