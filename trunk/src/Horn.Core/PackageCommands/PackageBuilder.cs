using System.Collections.Generic;
using System.Linq;
using Horn.Core.BuildEngines;
using Horn.Core.Dependencies;
using Horn.Core.Dsl;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;
using log4net;

namespace Horn.Core.PackageCommands
{
    public class PackageBuilder : IPackageCommand
    {

        private readonly IGet get;
        private readonly IProcessFactory processFactory;
        private static readonly ILog log = LogManager.GetLogger(typeof (PackageBuilder));


        public void Execute(IPackageTree packageTree, IDictionary<string, IList<string>> switches)
        {
            string packageName = GetPackageName(switches);

            if(!packageTree.BuildNodes().Select(x => x.Name).ToList().Contains(packageName))
                throw new UnkownInstallPackageException(string.Format("No package definition exists for {0}.", packageName));

            IPackageTree componentTree = packageTree.RetrievePackage(packageName);

            IDependencyTree dependencyTree = GetDependencyTree(componentTree);

            BuildDependencyTree(packageTree, dependencyTree, switches);

            log.InfoFormat("\nHorn has finished installing {0}.\n\n".ToUpper(), packageName);
        }



        protected virtual void BuildDependencyTree(IPackageTree packageTree, IDependencyTree dependencyTree, IDictionary<string, IList<string>> switches)
        {
            foreach (var nextTree in dependencyTree)
            {
                IBuildMetaData nextMetaData = GetBuildMetaData(nextTree);

                if (!switches.Keys.Contains("rebuildonly"))
                    ExecuteSourceControlGet(nextMetaData, nextTree);

                ExecutePrebuild(nextMetaData, nextTree);

                log.InfoFormat("\nHorn is building {0}.\n\n".ToUpper(), nextMetaData.BuildEngine);

                nextMetaData.BuildEngine.Build(processFactory, nextTree);
            }
        }

        protected virtual void ExecutePrebuild(IBuildMetaData metaData, IPackageTree packageTree)
        {
            if ((metaData.PrebuildCommandList == null) || (metaData.PrebuildCommandList.Count == 0))
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

        protected virtual string GetPackageName(IDictionary<string, IList<string>> switches)
        {
            string packageName = switches["install"][0];

            log.InfoFormat("installing {0}.\n", packageName);

            return packageName;
        }

        protected virtual IDependencyTree GetDependencyTree(IPackageTree componentTree)
        {
            return new DependencyTree(componentTree);
        }

        protected virtual void ExecuteSourceControlGet(IBuildMetaData buildMetaData, IPackageTree componentTree)
        {
            if ((buildMetaData.RepositoryElementList != null) && (buildMetaData.RepositoryElementList.Count > 0))
            {
                componentTree.DeleteWorkingDirectory();

                foreach (var repositoryElement in buildMetaData.RepositoryElementList)
                {
                    repositoryElement.PrepareRepository(componentTree, get).Export();
                }

                return;
            }
            
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



        public PackageBuilder(IGet get, IProcessFactory processFactory)
        {
            this.get = get;
            this.processFactory = processFactory;
        }



    }
}
