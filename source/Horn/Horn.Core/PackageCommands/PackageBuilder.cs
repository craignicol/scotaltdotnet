using System.Collections.Generic;
using Horn.Core.Get;
using Horn.Core.PackageStructure;

namespace Horn.Core.PackageCommands
{
    public class PackageBuilder : IPackageCommand
    {
        private readonly IGet get;

        public void Execute(IPackageTree packageTree, IDictionary<string, IList<string>> switches)
        {
            var packageName = switches["install"][0];

            var buildMetaData = packageTree.Retrieve(packageName).GetBuildMetaData();
        }

        public PackageBuilder(IGet get)
        {
            this.get = get;
        }
    }
}