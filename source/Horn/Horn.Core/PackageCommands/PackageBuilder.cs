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
            var buildMetaData = packageTree.Retrieve(switches["install"][0]).GetBuildMetaData();
        }

        public PackageBuilder(IGet get)
        {
            this.get = get;
        }
    }
}