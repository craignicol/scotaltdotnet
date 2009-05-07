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
        protected PackageTreeStub packageTree;

        protected override void Before_each_spec()
        {
            base.Before_each_spec();

            var log4NetTree = new PackageTreeStub(GetPackageTreeParts(new List<Dependency>()), "log4net", true);

            var booTree = new PackageTreeStub(GetPackageTreeParts(new List<Dependency>()), "boo", true);

            var castleDependencies = new List<Dependency>
                                             {
                                                 new Dependency("log4net", "log4net"),
                                                 new Dependency("boo", "Boo.Lang.Extensions"),
                                                 new Dependency("boo", "Boo.Lang.Interpreter"),
                                                 new Dependency("boo", "Boo.Lang.Parser"),
                                                 new Dependency("boo", "Boo.Lang.Useful"),
                                                 new Dependency("boo", "Boo.NAnt.Tasks"),
                                                 new Dependency("boo", "Boo.Lang.CodeDom"),
                                                 new Dependency("boo", "Boo.Lang.Compiler"),
                                                 new Dependency("boo", "booc"),
                                                 new Dependency("boo", "Boo.Lang"),
                                                 new Dependency("nhibernate", "nhibernate"),
                                                 new Dependency("nhibernate", "iesi")
                                             };

            var castleTree = new PackageTreeStub(GetPackageTreeParts(castleDependencies), "castle", true);

            castleTree.AddDependencyPackageTree("log4net", log4NetTree);

            castleTree.AddDependencyPackageTree("boo", booTree);

            var nhibernateDependencies = new List<Dependency>
                                             {
                                                 new Dependency("log4net", "log4net"),
                                                 new Dependency("castle", "Castle.Core"),
                                                 new Dependency("castle", "Castle.Core")
                                             };

            var nhibernateTree = new PackageTreeStub(GetPackageTreeParts(nhibernateDependencies), "nhibernate", true);

            nhibernateTree.AddDependencyPackageTree("castle", castleTree);
            nhibernateTree.AddDependencyPackageTree("log4net", log4NetTree);

            castleTree.AddDependencyPackageTree("nhibernate", nhibernateTree);

            var rootDependencies = new List<Dependency> {new Dependency("nhibernate", "nhibernate")};

            packageTree = new PackageTreeStub(GetPackageTreeParts(rootDependencies), "nhibernate.memcached", true);

            packageTree.AddDependencyPackageTree("nhibernate", nhibernateTree);
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

        //[Fact]
        public void Then_there_are_no_duplicates()
        {
            Assert.Equal(5, dependencyTree.BuildList.Count);      
        }

        //[Fact]
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