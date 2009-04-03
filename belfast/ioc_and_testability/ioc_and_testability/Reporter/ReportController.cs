using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ddd.belfast.ioc
{
    public class ReportController : Controller
    {
        private readonly ReportBuilder _reportBuilder;
        private readonly ReportSender _reportSender;

        public virtual bool SendAuditReports()
        {
            foreach(var report in _reportBuilder.CreateReports())
            {
                _reportSender.Send(report);
            }

            return true;
        }

        public ReportController()
        {
            _reportBuilder = new ReportBuilder();

            var emailSender = new EmailSender();

            _reportSender = new ReportSender(emailSender);
        }
    }
}