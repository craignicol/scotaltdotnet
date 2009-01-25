using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;
using log4net;
using System;

namespace Horn.Core.PackageCommands
{
    using dsl;
    using Horn.Core.Spec.Unit.DependencyTree;

    public class PackageBuilder : IPackageCommand
    {
        private readonly IGet get;
        private readonly IProcessFactory processFactory;

        private static readonly ILog log = LogManager.GetLogger(typeof (PackageBuilder));

        public void Execute(IPackageTree packageTree, IDictionary<string, IList<string>> switches)
        {
            string packageName = GetPackageName(switches);

            IPackageTree componentTree = GetComponentTreeFrom(packageTree, packageName);
            
            IBuildMetaData buildMetaData = GetBuildMetaDataFor(packageTree, packageName);

            IDependencyTree dependencyTree = GetDependencyTree(componentTree);

            foreach (IPackageTree nextTree in dependencyTree.BuildList)
            {
                IBuildMetaData nextMetaData = nextTree.GetBuildMetaData();

                log.InfoFormat("\nHorn is fetching {0}.\n\n".ToUpper(), nextMetaData.SourceControl.Url);

                ExecuteSourceControlGet(nextMetaData, nextTree);            	
            }

            foreach (IPackageTree nextTree in dependencyTree.BuildList)
            {
                IBuildMetaData nextMetaData = nextTree.GetBuildMetaData();

                log.InfoFormat("\nHorn is building {0}.\n\n".ToUpper(), nextMetaData.BuildEngine.BuildFile);

                BuildComponentTree(nextMetaData, nextTree);
            }

            log.InfoFormat("\nHorn has finished installing {0}.\n\n".ToUpper(), packageName);
        }

        private string GetPackageName(IDictionary<string, IList<string>> switches)
        {
            string packageName = switches["install"][0];

            log.InfoFormat("installing {0}.\n", packageName);

            return packageName;
        }

        private IPackageTree GetComponentTreeFrom(IPackageTree packageTree, string packageName)
        {
            return packageTree.Retrieve(packageName);
        }

        private IBuildMetaData GetBuildMetaDataFor(IPackageTree packageTree, string packageName)
        {
            return packageTree.Retrieve(packageName).GetBuildMetaData();
        }

        private IDependencyTree GetDependencyTree(IPackageTree componentTree)
        {
            return new DependencyTree(componentTree);
        }

        private void ExecuteSourceControlGet(IBuildMetaData buildMetaData, IPackageTree componentTree)
        {
            get.From(buildMetaData.SourceControl).ExportTo(componentTree.WorkingDirectory.FullName);
        }

        private void BuildComponentTree(IBuildMetaData buildMetaData, IPackageTree componentTree)
        {
            buildMetaData.BuildEngine.Build(processFactory, componentTree);
        }        

        public PackageBuilder(IGet get, IProcessFactory processFactory)
        {
            this.get = get;
            this.processFactory = processFactory;
        }
    }
}
