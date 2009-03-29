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

        string BuildFile { get; }

        void CreateRequiredDirectories();

        IPackageTree RetrievePackage(string packageName);

        IBuildMetaData GetBuildMetaData(string packageName);

        DirectoryInfo CurrentDirectory { get; }

        FileInfo Nant { get; }

        DirectoryInfo WorkingDirectory { get; }

        bool IsBuildNode { get; }
        
        DirectoryInfo OutputDirectory { get; }

        List<IPackageTree> BuildNodes();

        IRevisionData GetRevisionData();
    }
}