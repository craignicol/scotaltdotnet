namespace ddd.belfast.ioc
{
    public interface IReportSender
    {
        void Send(Report report);
    }

    public class ReportSender : IReportSender
    {
        private readonly IEmailSender _emailSender;

        public void Send(Report report)
        {
            var attachments = new[] {report.FilePath};

            _emailSender.Send("admin@somebody.com", 
                                "somepeople@somewhere.com", 
                                "report is ready", 
                                "Here are the reports",
                                attachments);
        }

        public ReportSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
    }
}