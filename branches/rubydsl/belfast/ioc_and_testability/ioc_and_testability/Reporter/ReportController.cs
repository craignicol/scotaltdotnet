using System.Web.Mvc;

namespace ddd.belfast.ioc
{
    public class ReportController : Controller
    {
        public virtual bool SendAuditReports()
        {
            var reportBuilder = new ReportBuilder();
            var emailSender = new EmailSender();
            var reportSender = new ReportSender(emailSender);

            foreach(var report in reportBuilder.CreateReports())
            {
                reportSender.Send(report);
            }

            return true;
        }
    }
}