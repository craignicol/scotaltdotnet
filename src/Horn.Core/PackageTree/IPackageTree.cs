using System.Collections.Generic;
using System.IO;
using Horn.Core.Dsl;

namespace Horn.Core.PackageStructure
{
    public interface IPackageTree : IComposite<IPackageTree>
    {
        string Name { get; }

        bool IsRoot { get; }

        Dictionary<string, string> BuildFiles { get; set; }

        IPackageTree Retrieve(string packageName);

        IBuildMetaData GetBuildMetaData(string packageName);

        IBuildMetaData GetBuildMetaData(string packageName, string buildFile);

        DirectoryInfo CurrentDirectory { get; }

        DirectoryInfo WorkingDirectory { get; }

        bool IsBuildNode { get; }
        
        DirectoryInfo OutputDirectory { get; }

        List<IPackageTree> BuildNodes();
    }
}