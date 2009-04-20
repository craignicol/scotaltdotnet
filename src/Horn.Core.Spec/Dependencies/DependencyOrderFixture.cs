using System;
using Horn.Core.Dependencies;
using Horn.Core.PackageStructure;
using Horn.Core.Spec.helpers;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.Dependencies
{
    public class When_resolving_a_dependency_tree : DirectorySpecificationBase
    {
        protected IDependencyTree dependencyTree;
        protected IPackageTree packageTree;

        protected override void Before_each_spec()
        {
            base.Before_each_spec();
            packageTree = TreeHelper.CreatePackageTreeNode("nhibernate.memcached", new[] { "nhibernate" });
            IPackageTree dependentTree = TreeHelper.CreatePackageTreeNode("nhibernate", new[] { "castle", "log4net" });

            IPackageTree[] packages = new[] { packageTree, dependentTree};

            foreach (IPackageTree packageStub in packages)
            {
                foreach (IPackageTree retrievedPackage in packages)
                {
                    packageStub.Stub(x => x.RetrievePackage(retrievedPackage.Name)).Return(retrievedPackage);
                }
            }
        }

        protected override void Because()
        {
        }

        [Fact]
        public void Then_there_are_no_duplicates()
        {
            
        }

        [Fact]
        public void Then_the_build_list_is_ordered_by_least_dependencies()
        {}
    }
}