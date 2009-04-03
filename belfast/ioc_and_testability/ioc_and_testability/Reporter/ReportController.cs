using System.Web.Mvc;

namespace ddd.belfast.ioc
{
    public class ReportController : Controller
    {
        private ReportBuilder _reportBuilder;
        private ReportSender _reportSender;

        public virtual bool SendAuditReports()
        {
            _reportBuilder = new ReportBuilder();
            var emailSender = new EmailSender();
            _reportSender = new ReportSender(emailSender);

            foreach(var report in _reportBuilder.CreateReports())
            {
                _reportSender.Send(report);
            }

            return true;
        }
    }
}