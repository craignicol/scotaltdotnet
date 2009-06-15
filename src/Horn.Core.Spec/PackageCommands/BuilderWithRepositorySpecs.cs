using System;
using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.Dsl;
using Horn.Core.GetOperations;
using Horn.Core.PackageCommands;
using Horn.Core.SCM;
using Horn.Core.Spec.Doubles;
using Horn.Core.Spec.helpers;
using Horn.Core.Spec.Unit.GetSpecs;
using Horn.Core.Utils;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.Unit.PackageCommands
{
    public class When_the_metadata_has_a_repository_get : GetSpecificationBase
    {
        private PackageBuilder packageBuilder;

        private MockRepository mockRepository;

        private readonly SourceControlDouble sourceControlDouble = new SourceControlDouble("url1");

        protected override void Before_each_spec()
        {
            mockRepository = new MockRepository();

            var castleElementOne = new RepositoryElement("castle", "Tools", "Tools");
            var caslteElementTwo = new RepositoryElement("castle", "Windsor", "Windsor");

            var repositoryIncludes = new List<RepositoryElement> {castleElementOne, caslteElementTwo};

            packageTree = new PackageTreeStub(TreeHelper.GetPackageTreeParts(new List<Dependency>(), repositoryIncludes), "castle", false);

            get = new Get(MockRepository.GenerateStub<IFileSystemProvider>());

            packageBuilder = new PackageBuilder(get, new DiagnosticsProcessFactory());
        }

        protected override void Because()
        {
            var args = new Dictionary<string, IList<string>>
                           {
                               {"install",  new List<string>{"castle"}}
                           };

            packageBuilder.Execute(packageTree, args);
        }

        [Fact]
        public void Then_the_parts_are_retrieved_from_the_repository()
        {
            
        }

        protected override void After_each_spec()
        {
            sourceControlDouble.Dispose();
        }
    }
}