namespace Horn.Core.Spec.Unit.DependencyTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Horn.Core.PackageStructure;
    using Horn.Core.BuildEngines;
    using Xunit;
    using System.IO;
    using Horn.Core.Spec.BuildEngine;
    using Horn.Core.dsl;
    using Rhino.Mocks;

    public class When_We_Have_A_Single_Dependency : DirectoryStructureSpecificationBase
    {
        protected IBuildMetaData dependencyBuildMetaData;
        protected IBuildMetaData rootBuildMetaData;
        protected IDependencyTree dependencyTree;
        protected IPackageTree packageTree;

        protected override void Because()
        {
            rootBuildMetaData = CreateStub<IBuildMetaData>();
            dependencyBuildMetaData = CreateStub<IBuildMetaData>();

            rootBuildMetaData.BuildEngine = new BuildEngine(new BuildToolStub(), "root.boo", Horn.Core.Utils.Framework.FrameworkVersion.frameworkVersion35);
            rootBuildMetaData.BuildEngine.Dependencies.Add(new Dependency("simpleDependency", "simpleDependency.boo"));
            dependencyBuildMetaData.BuildEngine = new BuildEngine(new BuildToolStub(), "simpleDependency.boo", Horn.Core.Utils.Framework.FrameworkVersion.frameworkVersion35);

            packageTree = CreateStub<IPackageTree>();
            packageTree.Stub(x => x.Name).Return("root");
            packageTree.Stub(x => x.GetBuildMetaData()).Return(rootBuildMetaData);

            IPackageTree dependentTree = CreateStub<IPackageTree>();
            dependentTree.Stub(x => x.Name).Return("simpleDependency");
            dependentTree.Stub(x => x.GetBuildMetaData()).Return(dependencyBuildMetaData);
            packageTree.Add(dependentTree);

            packageTree.Stub(x => x.Retrieve("")).IgnoreArguments().Return(dependentTree);
        }

        [Fact]
        public void Then_The_Dependency_Is_Built_Before_The_Root()
        {
            dependencyTree = new DependencyTree(rootBuildMetaData, packageTree);
            Assert.Contains(rootBuildMetaData, dependencyTree.BuildList);
            Assert.Contains(dependencyBuildMetaData, dependencyTree.BuildList);
            Assert.InRange(dependencyTree.BuildList.IndexOf(dependencyBuildMetaData), 0, dependencyTree.BuildList.IndexOf(rootBuildMetaData));
        }
    }
}
