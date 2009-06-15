using System;
using System.IO;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;

namespace Horn.Core.Dsl
{
    public class RepositoryElement
    {
        private IPackageTree repositoryTree;

        private IPackageTree packageToExportTo;

        public string ExportPath { get; private set; }

        public string IncludePath { get; private set; }

        public string RepositoryName { get; private set; }

        public virtual void Export()
        {   
            if(repositoryTree == null)
                throw new AccessViolationException("You must call PrepareRepository before export in the RepositoryElement class.");




        }

        public virtual RepositoryElement PrepareRepository(IPackageTree packageToExportTo, IGet get)
        {
            this.packageToExportTo = packageToExportTo;

            var buildMetaData = packageToExportTo.Root.GetBuildMetaData(RepositoryName);

            var repositoryTree = packageToExportTo.Root.RetrievePackage(RepositoryName);

            get.From(buildMetaData.SourceControl).ExportTo(repositoryTree);

            this.repositoryTree = repositoryTree;

            return this;
        }

        public RepositoryElement(string repositoryName, string includePath, string exportPath)
        {
            RepositoryName = repositoryName;
            IncludePath = includePath;
            ExportPath = exportPath;
        }
    }
}