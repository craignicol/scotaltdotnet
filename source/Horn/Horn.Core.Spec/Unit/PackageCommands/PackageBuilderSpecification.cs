using System.Collections.Generic;
using Horn.Core.dsl;
using Horn.Core.GetOperations;
using Horn.Core.PackageCommands;
using Horn.Core.SCM;
using Horn.Core.Spec.Unit.Get;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.Unit.PackageCommands
{
    public class When_The_Builder_Receives_An_Install_Switch : PackageCommandSpecificationBase
    {
        protected override void Because()
        {
            switches.Add("install", new List<string>{"horn"});

            get = new GetOperations.Get(fileSystemProvider);

            var dependencyResolver = CreateStub<IDependencyResolver>();

            buildConfigReader = new BuildConfigReader();

            dependencyResolver.Stub(x => x.Resolve<IBuildConfigReader>()).Return(buildConfigReader);

            var sourceControlDouble = new SourceControlDouble("https://svnserver/trunk");

            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>()).Return(sourceControlDouble);

            IoC.InitializeWith(dependencyResolver);
        }

        [Fact]
        public void Then_The_Builder_Coordinates_The_Build()
        {
            IPackageCommand command = new PackageBuilder(get);

            command.Execute(packageTree, switches);
        }
    }
}