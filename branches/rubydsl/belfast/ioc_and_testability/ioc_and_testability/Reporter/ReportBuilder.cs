using System.Collections.Generic;

namespace ddd.belfast.ioc
{
    public interface IReportBuilder
    {
        IList<Report> CreateReports();
    }

    public class ReportBuilder : IReportBuilder
    {
        public IList<Report> CreateReports()
        {
            var reports = new[] {new Report(1), new Report(2), new Report(3)};

            return new List<Report>(reports);
        }
    }
}