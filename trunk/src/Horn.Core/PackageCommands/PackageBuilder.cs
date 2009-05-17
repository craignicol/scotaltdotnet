using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Horn.Core.BuildEngines;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;
using log4net;

namespace Horn.Core.PackageCommands
{
    using Dependencies;
    using Dsl;
    using SCM;

    public class PackageBuilder : IPackageCommand
    {

        private readonly IGet get;
        private readonly IProcessFactory processFactory;
        private static readonly ILog log = LogManager.GetLogger(typeof (PackageBuilder));

        public void Execute(IPackageTree packageTree, IDictionary<string, IList<string>> switches)
        {
            string packageName = GetPackageName(switches);

            IPackageTree componentTree = packageTree.RetrievePackage(packageName);
            IDependencyTree dependencyTree = GetDependencyTree(componentTree);

            BuildDependencyTree(dependencyTree, switches);

            log.InfoFormat("\nHorn has finished installing {0}.\n\n".ToUpper(), packageName);
        }

        private void BuildDependencyTree(IDependencyTree dependencyTree, IDictionary<string, IList<string>> switches)
        {
            foreach (var nextTree in dependencyTree)
            {
                IBuildMetaData nextMetaData = GetBuildMetaData(nextTree);

                if (!switches.Keys.Contains("rebuildonly"))
                    ExecuteSourceControlGet(nextMetaData, nextTree);

                ExecutePrebuild(nextMetaData, nextTree);

                BuildComponentTree(nextMetaData.BuildEngine, nextTree);
            }
        }

        private void ExecutePrebuild(IBuildMetaData metaData, IPackageTree packageTree)
        {
            if ((metaData.PrebuildCommandList == null) || (metaData.PrebuildCommandList.Count == 0))
                return;

            foreach (var command in metaData.PrebuildCommandList)
            {
                processFactory.ExcuteCommand(command, packageTree.WorkingDirectory.FullName);
            }
        }

        private IBuildMetaData GetBuildMetaData(IPackageTree nextTree)
        {
            return nextTree.GetBuildMetaData(nextTree.BuildFile);
        }


        private string GetPackageName(IDictionary<string, IList<string>> switches)
        {
            string packageName = switches["install"][0];

            log.InfoFormat("installing {0}.\n", packageName);

            return packageName;
        }

        private IDependencyTree GetDependencyTree(IPackageTree componentTree)
        {
            return new DependencyTree(componentTree);
        }

        private void ExecuteSourceControlGet(IBuildMetaData buildMetaData, IPackageTree componentTree)
        {
            //TODO: Remove get_from functionality and only retrieve from the exportlist ??
            if((buildMetaData.ExportList != null) && (buildMetaData.ExportList.Count > 0))
            {
                var initialise = true;

                foreach (var sourceControl in buildMetaData.ExportList)
                {
                    log.InfoFormat("\nHorn is fetching {0}.\n\n".ToUpper(), sourceControl.Url);

                    get.From(sourceControl).ExportTo(componentTree, sourceControl.ExportPath, initialise);

                    initialise = false;
                }

                return;
            }

            log.InfoFormat("\nHorn is fetching {0}.\n\n".ToUpper(), buildMetaData.SourceControl.Url);
            get.From(buildMetaData.SourceControl).ExportTo(componentTree);
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
