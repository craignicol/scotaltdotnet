using System;
using Horn.Domain.BuildEngines;
using Horn.Domain.DependencyTree;
using Horn.Domain.Dsl;
using Horn.Domain.Framework;
using Horn.Domain.PackageStructure;
using Horn.Domain.Spec.BuildEngine;
using Rhino.Mocks;
using Xunit;

namespace Horn.Domain.Spec.Unit.TreeDependency
{
    public class When_We_Have_A_Single_Dependency : DirectorySpecificationBase
    {
        protected IBuildMetaData dependencyBuildMetaData;
        protected IBuildMetaData rootBuildMetaData;
        protected IDependencyTree dependencyTree;
        protected IPackageTree packageTree;
        protected IPackageTree dependentTree;

        protected override void Because()
        {
            rootBuildMetaData = CreateStub<IBuildMetaData>();
            dependencyBuildMetaData = CreateStub<IBuildMetaData>();

            rootBuildMetaData.BuildEngine = new BuildEngines.BuildEngine(new BuildToolStub(), "root.boo",  Framework.FrameworkVersion.FrameworkVersion35);
            rootBuildMetaData.BuildEngine.Dependencies.Add(new Dependency("simpleDependency", "simpleDependency"));
            dependencyBuildMetaData.BuildEngine = new BuildEngines.BuildEngine(new BuildToolStub(), "simpleDependency", Framework.FrameworkVersion.FrameworkVersion35);

            packageTree = CreateStub<IPackageTree>();
            packageTree.Stub(x => x.Name).Return("root");
            packageTree.Stub(x => x.GetBuildMetaData("root")).Return(rootBuildMetaData);

            dependentTree = CreateStub<IPackageTree>();
            dependentTree.Stub(x => x.Name).Return("simpleDependency");
            dependentTree.Stub(x => x.GetBuildMetaData("simpleDependency")).Return(dependencyBuildMetaData);

            packageTree.Stub(x => x.RetrievePackage("")).IgnoreArguments().Return(dependentTree);

            dependencyTree = new Domain.DependencyTree.DependencyTree(packageTree);
        }

        [Fact]
        public void Then_The_Dependency_Is_Built_Before_The_Root()
        {
            Assert.Contains(packageTree, dependencyTree.BuildList);
            Assert.Contains(dependentTree, dependencyTree.BuildList);
            Assert.InRange(dependencyTree.BuildList.IndexOf(dependentTree), 0, dependencyTree.BuildList.IndexOf(packageTree));
        }
    }

    public class When_We_Have_A_Circular_Dependency : DirectorySpecificationBase
    {
        protected IBuildMetaData dependencyBuildMetaData;
        protected IBuildMetaData rootBuildMetaData;
        protected IDependencyTree dependencyTree;
        protected IPackageTree packageTree;
        protected IPackageTree dependentTree;

        protected override void Because()
        {
            rootBuildMetaData = CreateStub<IBuildMetaData>();
            dependencyBuildMetaData = CreateStub<IBuildMetaData>();

            rootBuildMetaData.BuildEngine = new BuildEngines.BuildEngine(new BuildToolStub(), "root.boo", Framework.FrameworkVersion.FrameworkVersion35);
            rootBuildMetaData.BuildEngine.Dependencies.Add(new Dependency("simpleDependency", "simpleDependency.boo"));
            dependencyBuildMetaData.BuildEngine = new BuildEngines.BuildEngine(new BuildToolStub(), "simpleDependency.boo", Framework.FrameworkVersion.FrameworkVersion35);
            dependencyBuildMetaData.BuildEngine.Dependencies.Add(new Dependency("root", "root.boo"));

            packageTree = CreateStub<IPackageTree>();
            packageTree.Stub(x => x.Name).Return("root");
            packageTree.Stub(x => x.GetBuildMetaData("root")).Return(rootBuildMetaData);

            dependentTree = CreateStub<IPackageTree>();
            dependentTree.Stub(x => x.Name).Return("simpleDependency");
            dependentTree.Stub(x => x.GetBuildMetaData("simpleDependency")).Return(dependencyBuildMetaData);

            packageTree.Stub(x => x.RetrievePackage("simpleDependency")).Return(dependentTree);
            packageTree.Stub(x => x.RetrievePackage("root")).Return(packageTree);
            dependentTree.Stub(x => x.RetrievePackage("simpleDependency")).Return(dependentTree);
            dependentTree.Stub(x => x.RetrievePackage("root")).Return(packageTree);
        }

        [Fact]
        public void Then_An_Exception_Is_Raised()
        {
            Exception ex = Assert.Throws<CircularDependencyException>(() => new DependencyTree.DependencyTree(packageTree));
            Assert.Equal("root is a dependent of itself", ex.Message);
        }
    }

    public class When_We_Have_A_Complex_Dependency : DirectorySpecificationBase
    {
        protected IDependencyTree dependencyTree;
        protected IPackageTree packageTree;

        private IPackageTree CreatePackageTreeNode(string packageName, string[] dependencyNames)
        {
            var buildMetaData = CreateStub<IBuildMetaData>();
            buildMetaData.BuildEngine = new BuildEngines.BuildEngine(new BuildToolStub(), String.Format("{0}.boo", packageName), FrameworkVersion.FrameworkVersion35);
            foreach (string dependencyName in dependencyNames)
            {
                buildMetaData.BuildEngine.Dependencies.Add(new Dependency(dependencyName, String.Format("{0}", dependencyName)));                
            }

            var packageTree = CreateStub<IPackageTree>();
            packageTree.Stub(x => x.Name).Return(packageName);
            packageTree.Stub(x => x.GetBuildMetaData("root")).Return(buildMetaData);
            packageTree.Stub(x => x.GetBuildMetaData("complexDependency")).Return(buildMetaData);
            packageTree.Stub(x => x.GetBuildMetaData("sharedDependency")).Return(buildMetaData);

            return packageTree;

        }

        protected override void Because()
        {
            packageTree = CreatePackageTreeNode("root", new string[] {"complexDependency", "sharedDependency"});
            IPackageTree dependentTree = CreatePackageTreeNode("complexDependency", new[] {"sharedDependency"});
            IPackageTree sharedTree = CreatePackageTreeNode("sharedDependency", new string[] { });

            IPackageTree[] packages = new[] { packageTree, dependentTree, sharedTree };

            foreach (IPackageTree packageStub in packages)
            {
                foreach (IPackageTree retrievedPackage in packages)
                {
                    packageStub.Stub(x => x.RetrievePackage(retrievedPackage.Name)).Return(retrievedPackage);                    
                }
            }
        }

        [Fact]
        public void Then_No_Exception_Is_Raised()
        {
            dependencyTree = new DependencyTree.DependencyTree(packageTree);
            Assert.NotNull(dependencyTree);
        }
    }
}
