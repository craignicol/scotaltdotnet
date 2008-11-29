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

        IBuildMetaData GetBuildMetaData();

        DirectoryInfo CurrentDirectory { get; }

        DirectoryInfo WorkingDirectory { get; }

        bool IsBuildNode { get; }
        
        DirectoryInfo OutputDirectory { get; }

        List<IPackageTree> BuildNodes();
    }
}