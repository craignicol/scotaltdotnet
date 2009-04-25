using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.Dependencies;
using Horn.Core.Dsl;
using Horn.Core.PackageStructure;
using Horn.Core.Spec.Doubles;
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

            var log4NetTree = new PackageTreeStub(GetPackageTreeParts(new List<Dependency>()), "log4net", null);

            var castleDependencies = new List<Dependency>
                                             {
                                                 new Dependency("log4net", "log4net")
                                             };

            var castleTree = new PackageTreeStub(GetPackageTreeParts(castleDependencies), "castle", log4NetTree);

            var nhibernateDependencies = new List<Dependency>
                                             {
                                                 new Dependency("log4net", "log4net"),
                                                 new Dependency("castle", "Castle.Core"),
                                                 new Dependency("castle", "Castle.Core")
                                             };

            var nhibernateTree = new PackageTreeStub(GetPackageTreeParts(nhibernateDependencies), "nhibernate", castleTree);

            var rootDependencies = new List<Dependency> {new Dependency("nhibernate", "nhibernate")};

            packageTree = new PackageTreeStub(GetPackageTreeParts(rootDependencies), "nhibernate.memcached", nhibernateTree);
        }

        private IBuildMetaData GetPackageTreeParts(List<Dependency> dependencies)
        {
            var buildEngine = new BuildEngineStub(null, null, dependencies);
            var sourceControl = new SourceControlDouble("http://someurl.com");
            return new BuildMetaDataStub(buildEngine, sourceControl);
        }

        protected override void Because()
        {
            dependencyTree = new DependencyTree(packageTree);
        }

        [Fact]
        public void Then_there_are_no_duplicates()
        {
            Assert.Equal(4, dependencyTree.BuildList.Count);      
        }

        [Fact]
        public void Then_the_build_list_is_ordered_by_least_dependencies()
        {
            var buildList = new List<IPackageTree>(dependencyTree.BuildList);

            Assert.Equal("log4net", buildList[0].Name);
            Assert.Equal("castle", buildList[1].Name);
            Assert.Equal("nhibernate", buildList[2].Name);
            Assert.Equal("nhibernate.memcached", buildList[3].Name);   
        }
    }
}