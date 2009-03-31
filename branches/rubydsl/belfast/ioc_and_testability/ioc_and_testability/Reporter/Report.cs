using System;
using System.IO;

namespace ddd.belfast.ioc
{
    public class Report
    {

        public DateTime Created { get; set; }

        public string FilePath { get; private set; }

        public int Id { get; private set; }

        public Report(int id)
        {
            Id = id;

            FilePath = Path.Combine(@"C:\reports\{0}\report.doc", Id.ToString());
        }
    }
}