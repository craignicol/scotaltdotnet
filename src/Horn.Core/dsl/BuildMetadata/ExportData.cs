using System;
using System.IO;

namespace Horn.Core.Dsl
{
    public enum SourceControlType
    {
        svn
    }

    public class ExportData
    {
        public string Path { get; private set; }

        public string Url { get; private set; }

        public SourceControlType SourceControlType { get; private set; }

        public ExportData(string url, SourceControlType sourceControlType)
        {
            Url = url;
            SourceControlType = sourceControlType;
        }

        public ExportData(string url, SourceControlType sourceControlType, string path) : this(url, sourceControlType)
        {
            Path = path;
        }
    }
}