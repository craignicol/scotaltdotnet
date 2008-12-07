using System.Collections.Generic;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;
using log4net;

namespace Horn.Core.PackageCommands
{
    using dsl;

    public class PackageBuilder : IPackageCommand
    {
        private readonly IGet get;

        private static readonly ILog log = LogManager.GetLogger(typeof (PackageBuilder));

        public void Execute(IPackageTree packageTree, IDictionary<string, IList<string>> switches)
        {
            string packageName = GetPackageName(switches);

            IPackageTree componentTree = GetComponentTreeFrom(packageTree, packageName);
            
            IBuildMetaData buildMetaData = GetBuildMetaDataFor(packageTree, packageName);

            ExecuteSourceControlGet(buildMetaData, componentTree);
            
            BuildComponentTree(buildMetaData, componentTree);
        }

        private string GetPackageName(IDictionary<string, IList<string>> switches)
        {
            string packageName = switches["install"][0];

            log.InfoFormat("installing {0}.\n", packageName);

            return packageName;
        }

        private IPackageTree GetComponentTreeFrom(IPackageTree packageTree, string packageName)
        {
            return packageTree.Retrieve(packageName);
        }

        private IBuildMetaData GetBuildMetaDataFor(IPackageTree packageTree, string packageName)
        {
            return packageTree.Retrieve(packageName).GetBuildMetaData();
        }

        private void ExecuteSourceControlGet(IBuildMetaData buildMetaData, IPackageTree componentTree)
        {
            get.From(buildMetaData.SourceControl).ExportTo(componentTree.WorkingDirectory.FullName);
        }

        private void BuildComponentTree(IBuildMetaData buildMetaData, IPackageTree componentTree)
        {
            buildMetaData.BuildEngine.Build(componentTree);
        }        

        public PackageBuilder(IGet get)
        {
            this.get = get;
        }
    }
}
