using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;
using log4net;

namespace Horn.Core.PackageCommands
{
    using Dependencies;
    using Dsl;
    using SCM;
    using Utils.CmdLine;

    public class PackageBuilder : IPackageCommand
    {

        private readonly IGet get;
        private readonly IProcessFactory processFactory;
        private static readonly ILog log = LogManager.GetLogger(typeof (PackageBuilder));

        public void Execute(IPackageTree packageTree, CommandLineArguments switches)
        {
            string packageName = GetPackageName(switches);

            IPackageTree componentTree = packageTree.RetrievePackage(packageName);
            IDependencyTree dependencyTree = GetDependencyTree(componentTree);

            BuildDependencyTree(dependencyTree);

            log.InfoFormat("\nHorn has finished installing {0}.\n\n".ToUpper(), packageName);
        }

        private void BuildDependencyTree(IDependencyTree dependencyTree)
        {
            foreach (var nextTree in dependencyTree)
            {
                IBuildMetaData nextMetaData = GetBuildMetaData(nextTree);
                ExecuteSourceControlGet(nextMetaData.SourceControl, nextTree);
                BuildComponentTree(nextMetaData.BuildEngine, nextTree);
            }
        }

        private IBuildMetaData GetBuildMetaData(IPackageTree nextTree)
        {
            return nextTree.GetBuildMetaData(nextTree.BuildFile);
        }


        private string GetPackageName(CommandLineArguments commandLineArguments)
        {
            string packageName = commandLineArguments.PackageName;

            log.InfoFormat("installing {0}.\n", packageName);

            return packageName;
        }

        private IDependencyTree GetDependencyTree(IPackageTree componentTree)
        {
            return new DependencyTree(componentTree);
        }

        private void ExecuteSourceControlGet(SourceControl sourceControl, IPackageTree componentTree)
        {
            log.InfoFormat("\nHorn is fetching {0}.\n\n".ToUpper(), sourceControl.Url);
            get.From(sourceControl).ExportTo(componentTree);
        }

        private void BuildComponentTree(BuildEngine buildEngine, IPackageTree componentTree)
        {
            log.InfoFormat("\nHorn is building {0}.\n\n".ToUpper(), buildEngine.BuildFile);
            buildEngine.Build(processFactory, componentTree);
        }

        public PackageBuilder(IGet get, IProcessFactory processFactory)
        {
            this.get = get;
            this.processFactory = processFactory;
        }
    }
}
