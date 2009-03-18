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
    using Horn.Core.Dsl;
    using Rhino.Mocks;
    using Horn.Core.DependencyTree;

    public class When_We_Have_A_Single_Dependency : DirectoryStructureSpecificationBase
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

            rootBuildMetaData.BuildEngine = new BuildEngine(new BuildToolStub(), "root.boo", Horn.Core.Utils.Framework.FrameworkVersion.FrameworkVersion35);
            rootBuildMetaData.BuildEngine.Dependencies.Add(new Dependency("simpleDependency", "simpleDependency.boo"));
            dependencyBuildMetaData.BuildEngine = new BuildEngine(new BuildToolStub(), "simpleDependency.boo", Horn.Core.Utils.Framework.FrameworkVersion.FrameworkVersion35);

            packageTree = CreateStub<IPackageTree>();
            packageTree.Stub(x => x.Name).Return("root");
            packageTree.Stub(x => x.GetBuildMetaData()).Return(rootBuildMetaData);

            dependentTree = CreateStub<IPackageTree>();
            dependentTree.Stub(x => x.Name).Return("simpleDependency");
            dependentTree.Stub(x => x.GetBuildMetaData()).Return(dependencyBuildMetaData);

            packageTree.Stub(x => x.Retrieve("")).IgnoreArguments().Return(dependentTree);
        }

        [Fact]
        public void Then_The_Dependency_Is_Built_Before_The_Root()
        {
            dependencyTree = new DependencyTree(packageTree);
            Assert.Contains(packageTree, dependencyTree.BuildList);
            Assert.Contains(dependentTree, dependencyTree.BuildList);
            Assert.InRange(dependencyTree.BuildList.IndexOf(dependentTree), 0, dependencyTree.BuildList.IndexOf(packageTree));
        }
    }

    public class When_We_Have_A_Circular_Dependency : DirectoryStructureSpecificationBase
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

            rootBuildMetaData.BuildEngine = new BuildEngine(new BuildToolStub(), "root.boo", Horn.Core.Utils.Framework.FrameworkVersion.FrameworkVersion35);
            rootBuildMetaData.BuildEngine.Dependencies.Add(new Dependency("simpleDependency", "simpleDependency.boo"));
            dependencyBuildMetaData.BuildEngine = new BuildEngine(new BuildToolStub(), "simpleDependency.boo", Horn.Core.Utils.Framework.FrameworkVersion.FrameworkVersion35);
            dependencyBuildMetaData.BuildEngine.Dependencies.Add(new Dependency("root", "root.boo"));

            packageTree = CreateStub<IPackageTree>();
            packageTree.Stub(x => x.Name).Return("root");
            packageTree.Stub(x => x.GetBuildMetaData()).Return(rootBuildMetaData);

            dependentTree = CreateStub<IPackageTree>();
            dependentTree.Stub(x => x.Name).Return("simpleDependency");
            dependentTree.Stub(x => x.GetBuildMetaData()).Return(dependencyBuildMetaData);

            packageTree.Stub(x => x.Retrieve("simpleDependency")).Return(dependentTree);
            packageTree.Stub(x => x.Retrieve("root")).Return(packageTree);
            dependentTree.Stub(x => x.Retrieve("simpleDependency")).Return(dependentTree);
            dependentTree.Stub(x => x.Retrieve("root")).Return(packageTree);
        }

        [Fact]
        public void Then_An_Exception_Is_Raised()
        {
            Exception ex = Assert.Throws<CircularDependencyException>(() => new DependencyTree(packageTree));
            Assert.Equal("root is a dependent of itself", ex.Message);
        }
    }

    public class When_We_Have_A_Complex_Dependency : DirectoryStructureSpecificationBase
    {
        protected IDependencyTree dependencyTree;
        protected IPackageTree packageTree;

        private IPackageTree CreatePackageTreeNode(string packageName, string[] dependencyNames)
        {
            IBuildMetaData buildMetaData = CreateStub<IBuildMetaData>();
            buildMetaData.BuildEngine = new BuildEngine(new BuildToolStub(), String.Format("{0}.boo", packageName), Horn.Core.Utils.Framework.FrameworkVersion.FrameworkVersion35);
            foreach (string dependencyName in dependencyNames)
            {
                buildMetaData.BuildEngine.Dependencies.Add(new Dependency(dependencyName, String.Format("{0}.boo", dependencyName)));                
            }

            IPackageTree packageTree = CreateStub<IPackageTree>();
            packageTree.Stub(x => x.Name).Return(packageName);
            packageTree.Stub(x => x.GetBuildMetaData()).Return(buildMetaData);

            return packageTree;
        }

        protected override void Because()
        {
            packageTree = CreatePackageTreeNode("root", new string[] {"complexDependency", "sharedDependency"});
            IPackageTree dependentTree = CreatePackageTreeNode("complexDependency", new string[] {"sharedDependency"});
            IPackageTree sharedTree = CreatePackageTreeNode("sharedDependency", new string[] { });

            IPackageTree[] packages = new IPackageTree[] { packageTree, dependentTree, sharedTree };

            foreach (IPackageTree packageStub in packages)
            {
                foreach (IPackageTree retrievedPackage in packages)
                {
                    packageStub.Stub(x => x.Retrieve(retrievedPackage.Name)).Return(retrievedPackage);                    
                }
            }
        }

        [Fact]
        public void Then_No_Exception_Is_Raised()
        {
            dependencyTree = new DependencyTree(packageTree);
            Assert.NotNull(dependencyTree);
        }
    }
}
