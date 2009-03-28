using System.Collections.Generic;
using System.IO;
using Horn.Core.Dsl;

namespace Horn.Core.PackageStructure
{
    public interface IPackageTree : IComposite<IPackageTree>
    {
        string Name { get; }

        bool IsRoot { get; }

        bool Exists { get; }

        Dictionary<string, string> BuildFiles { get; set; }

        void CreateRequiredDirectories();

        IPackageTree RetrievePackage(string packageName);

        IBuildMetaData GetBuildMetaData(string packageName);

        IBuildMetaData GetBuildMetaData(string packageName, string buildFile);

        DirectoryInfo CurrentDirectory { get; }

        DirectoryInfo WorkingDirectory { get; }

        bool IsBuildNode { get; }
        
        DirectoryInfo OutputDirectory { get; }

        List<IPackageTree> BuildNodes();

        IRevisionData GetRevisionData();
    }
}