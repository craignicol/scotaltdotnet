using System;
using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.Dependencies;
using Horn.Core.Dsl;
using Horn.Core.PackageStructure;
using Horn.Core.Spec.BuildEngine;
using Horn.Core.Spec.Doubles;
using Horn.Framework.helpers;
using Rhino.Mocks;

namespace Horn.Core.Spec.helpers
{
    public static class TreeHelper
    {
        public static IPackageTree GetTempEmptyPackageTree()
        {
            var treeDirectory = PackageTreeHelper.CreateEmptyDirectoryStructureForTesting();

            return new PackageTree(treeDirectory, null);            
        }

        public static IPackageTree GetTempPackageTree()
        {
            var treeDirectory =  PackageTreeHelper.CreateDirectoryStructureForTesting();

            return new PackageTree(treeDirectory, null);
        }

        public static IBuildMetaData GetPackageTreeParts(List<Dependency> dependencies)
        {
            var buildEngine = new BuildEngineStub(null, null, dependencies);
            var sourceControl = new SourceControlDouble("http://someurl.com");
            return new BuildMetaDataStub(buildEngine, sourceControl);
        }

        public static IPackageTree CreatePackageTreeNode(string packageName, string[] dependencyNames)
        {
            var buildMetaData = MockRepository.GenerateStub<IBuildMetaData>();
            buildMetaData.BuildEngine = new BuildEngines.BuildEngine(new BuildToolStub(), String.Format("{0}.boo", packageName), Utils.Framework.FrameworkVersion.FrameworkVersion35, MockRepository.GenerateStub<IDependencyDispatcher>());
            foreach (string dependencyName in dependencyNames)
            {
                buildMetaData.BuildEngine.Dependencies.Add(new Dependency(dependencyName, String.Format("{0}", dependencyName)));
            }

            var packageTree = MockRepository.GenerateStub<IPackageTree>();
            packageTree.Stub(x => x.Name).Return(packageName);
            packageTree.Stub(x => x.GetBuildMetaData("root")).Return(buildMetaData);
            packageTree.Stub(x => x.GetBuildMetaData("complexDependency")).Return(buildMetaData);
            packageTree.Stub(x => x.GetBuildMetaData("sharedDependency")).Return(buildMetaData);

            return packageTree;

        }
    }
}