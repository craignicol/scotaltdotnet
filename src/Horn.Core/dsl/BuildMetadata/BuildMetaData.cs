using System;
using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.SCM;

namespace Horn.Core.Dsl
{
    public class BuildMetaData : IBuildMetaData
    {

        public string InstallName { get; set; }

        public BuildEngine BuildEngine { get; set; }

        public string Description { get; set; }

        public List<SourceControl> ExportList{ get; set; }

        public List<RepositoryInclude> IncludeList { get; set; }

        public List<string> PrebuildCommandList { get; set; }

        public Dictionary<string, object> ProjectInfo { get; set; }

        public SourceControl SourceControl { get; set; }


        public BuildMetaData()
        {
            ProjectInfo = new Dictionary<string, object>();

            ExportList = new List<SourceControl>();

            IncludeList = new List<RepositoryInclude>();
        }

        //public BuildMetaData(BooConfigReader instance) : this()
        //{
            //BuildEngine = instance.BuildEngine;

            //SourceControl = instance.SourceControl;
            //BuildEngine.OutputDirectory = instance.BuildEngine.OutputDirectory;
            //BuildEngine.SharedLibrary = instance.BuildEngine.SharedLibrary;
            //ProjectInfo = instance.PackageMetaData.PackageInfo;
            //PrebuildCommandList = instance.PrebuildCommandList;

            //ExportList = new List<SourceControl>();

            //foreach (var exportData in instance.ExportList)
            //{
            //    switch (exportData.SourceControlType)
            //    {
            //        case SourceControlType.svn:
            //            ExportList.Add(new SVNSourceControl(exportData.Url, exportData.Path));
            //            break;
            //        default:
            //            throw new ArgumentOutOfRangeException(string.Format("Unkown SourceControlType {0}",
            //                                                                exportData.SourceControlType));
            //    }
            //}
        //}



    }
}