using System.Collections.Generic;
using System.IO;
using Horn.Core.dsl;

namespace Horn.Core.PackageStructure
{
    public interface IPackageTree : IComposite<IPackageTree>
    {
        string Name { get; }

        bool IsRoot { get; }

        IPackageTree Retrieve(string packageName);

        BuildMetaData GetBuildMetaData();

        DirectoryInfo CurrentDirectory { get; }

        bool IsBuildNode { get; }

        List<IPackageTree> BuildNodes();
    }
}