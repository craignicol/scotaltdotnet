using System.Collections.Generic;
using System.Linq;
using Horn.Core.BuildEngines;
using Horn.Core.Dependencies;
using Horn.Core.Dsl;
using Horn.Core.extensions;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.CmdLine;
using log4net;

namespace Horn.Core.PackageCommands
{
    public class PackageBuilder : IPackageCommand
    {
        private readonly IGet get;
        private readonly IProcessFactory processFactory;
        private readonly ICommandArgs commandArgs;
        private static readonly ILog log = LogManager.GetLogger(typeof(PackageBuilder));

        public void Execute(IPackageTree packageTree)
        {
            string packageName = GetPackageName();

            if (!packageTree.BuildNodes().Select(x => x.Name).ToList().Contains(packageName))
                throw new UnkownInstallPackageException(string.Format("No package definition exists for {0}.", packageName));

            IPackageTree componentTree = packageTree.RetrievePackage(packageName);

            IDependencyTree dependencyTree = GetDependencyTree(componentTree);

            BuildDependencyTree(packageTree, dependencyTree);

            log.InfoFormat("\nHorn has finished installing {0}.\n\n".ToUpper(), packageName);
        }

        protected virtual void BuildDependencyTree(IPackageTree packageTree, IDependencyTree dependencyTree)
        {
            foreach (var nextTree in dependencyTree)
            {
                IBuildMetaData nextMetaData = GetBuildMetaData(nextTree);

                if (!commandArgs.RebuildOnly)
                    RetrieveSourceCode(nextMetaData, nextTree);

                ExecutePrebuildCommands(nextMetaData, nextTree);

                BuildSource(nextTree, nextMetaData);
            }
        }

        protected virtual void BuildSource(IPackageTree nextTree, IBuildMetaData nextMetaData)
        {
            log.InfoFormat("\nHorn is building {0}.\n\n".ToUpper(), nextMetaData.BuildEngine);

            nextMetaData.BuildEngine.Build(processFactory, nextTree);
        }

        protected virtual void ExecutePrebuildCommands(IBuildMetaData metaData, IPackageTree packageTree)
        {
            if (!metaData.PrebuildCommandList.HasElements())
                return;

            foreach (var command in metaData.PrebuildCommandList)
            {
                processFactory.ExcuteCommand(command, packageTree.WorkingDirectory.FullName);
            }
        }

        protected virtual IBuildMetaData GetBuildMetaData(IPackageTree nextTree)
        {
            return nextTree.GetBuildMetaData(nextTree.BuildFile);
        }

        protected virtual string GetPackageName()
        {
            string packageName = commandArgs.InstallName;

            log.InfoFormat("installing {0}.\n", packageName);

            return packageName;
        }

        protected virtual IDependencyTree GetDependencyTree(IPackageTree componentTree)
        {
            return new DependencyTree(componentTree);
        }

        protected virtual void RetrieveSourceCode(IBuildMetaData buildMetaData, IPackageTree componentTree)
        {
            ExecuteRepositoryElementList(buildMetaData, componentTree);

            ExecuteExportList(buildMetaData, componentTree);

            if (buildMetaData.SourceControl == null)
                return;

            log.InfoFormat("\nHorn is fetching {0}.\n\n".ToUpper(), buildMetaData.SourceControl.Url);

            get.From(buildMetaData.SourceControl).ExportTo(componentTree);
        }

        protected virtual void ExecuteExportList(IBuildMetaData buildMetaData, IPackageTree componentTree)
        {
            if (!buildMetaData.ExportList.HasElements())
                return;

            var initialise = true;

            foreach (var sourceControl in buildMetaData.ExportList)
            {
                log.InfoFormat("\nHorn is fetching {0}.\n\n".ToUpper(), sourceControl.Url);

                get.From(sourceControl).ExportTo(componentTree, sourceControl.ExportPath, initialise);

                initialise = false;
            }
        }

        protected virtual void ExecuteRepositoryElementList(IBuildMetaData buildMetaData, IPackageTree componentTree)
        {
            if (!buildMetaData.RepositoryElementList.HasElements())
                return;

            componentTree.DeleteWorkingDirectory();

            foreach (var repositoryElement in buildMetaData.RepositoryElementList)
            {
                repositoryElement.PrepareRepository(componentTree, get).Export();
            }
        }

        public PackageBuilder(IGet get, IProcessFactory processFactory, ICommandArgs commandArgs)
        {
            this.get = get;
            this.processFactory = processFactory;
            this.commandArgs = commandArgs;
        }
    }
}
