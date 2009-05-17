using System;
using System.IO;

namespace Horn.Core.Dsl
{
    public enum SourceControlType
    {
        Svn
    }

    public class ExportData
    {
        public DirectoryInfo To { get; private set; }

        public string Url { get; private set; }

        public SourceControlType SourceControlType { get; private set; }

        public ExportData(string url, SourceControlType sourceControlType)
        {
            Url = url;
            SourceControlType = sourceControlType;
        }

        public ExportData(string url, string path, SourceControlType sourceControlType) : this(url, sourceControlType)
        {
            To = new DirectoryInfo(path);
        }
    }
}