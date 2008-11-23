using System.Collections.Generic;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;

namespace Horn.Core.PackageCommands
{
    public class PackageBuilder : IPackageCommand
    {
        private readonly IGet get;

        public void Execute(IPackageTree packageTree, IDictionary<string, IList<string>> switches)
        {
            var packageName = switches["install"][0];

            var componentTree = packageTree.Retrieve(packageName);

            var buildMetaData = packageTree.Retrieve(packageName).GetBuildMetaData();

            get.From(buildMetaData.SourceControl).ExportTo(componentTree.WorkingDirectory.FullName);
        }

        public PackageBuilder(IGet get)
        {
            this.get = get;
        }
    }
}