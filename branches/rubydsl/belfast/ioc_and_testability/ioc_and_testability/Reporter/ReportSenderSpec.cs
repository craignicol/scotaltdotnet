using System;
using NUnit.Framework;

namespace ddd.belfast.ioc
{
    [TestFixture]
    public class When_the_audit_reports_are_requested : ContextSpecification
    {
        private ReportController _reportController;

        private bool _result;
        protected override void establish_context()
        {
            _reportController = new ReportController();
        }

        protected override void because()
        {
            _result = _reportController.SendAuditReports();
        }

        [Test]
        public void Then_the_reports_are_distributed()
        {
            Assert.That(_result, Is.True);
        }
    }
}