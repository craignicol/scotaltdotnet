using System;
using Horn.Core.SCM;

namespace Horn.Core.Dsl
{
    public class ExportData
    {
        public SourceControl SourceControl { get; private set; }

        public ExportData(string url, string sourceControlType)
        {
            switch (sourceControlType.ToLower())
            {
                case "svn":
                    SourceControl= new SVNSourceControl(url, null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Unkown SourceControlType {0}",
                                                                        sourceControlType));
            }
        }

        public ExportData(string url, string sourceControlType, string path)
        {
            switch (sourceControlType.ToLower())
            {
                case "svn":
                    SourceControl = new SVNSourceControl(url, path);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Unkown SourceControlType {0}",
                                                                        sourceControlType));
            }
        }
    }
}