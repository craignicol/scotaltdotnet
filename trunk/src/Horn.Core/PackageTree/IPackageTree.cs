using System.Collections.Generic;
using System.IO;
using Horn.Core.Dsl;

namespace Horn.Core.PackageStructure
{
    public interface IPackageTree : IComposite<IPackageTree>
    {
        string BuildFile { get; }

        IBuildMetaData BuildMetaData { get; }

        DirectoryInfo CurrentDirectory { get; }

        bool Exists { get; }

        string FullName { get; }

        bool IsBuildNode { get; }

        bool IsRoot { get; }

        string Name { get; }

        FileInfo Nant { get; }

        DirectoryInfo OutputDirectory { get; }

        DirectoryInfo Result { get; }

        IPackageTree Root { get; }

        FileInfo Sn { get; }

        DirectoryInfo WorkingDirectory { get; }

        string Version { get; set; }

        void CreateRequiredDirectories();

        void DeleteWorkingDirectory();

        List<IPackageTree> BuildNodes();

        IBuildMetaData GetBuildMetaData(string packageName);

        IRevisionData GetRevisionData();

        IPackageTree GetRootPackageTree(DirectoryInfo rootFolder);

        IPackageTree RetrievePackage(string packageName);
    }
}