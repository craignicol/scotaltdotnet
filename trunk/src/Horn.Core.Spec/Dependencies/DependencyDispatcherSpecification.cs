using System.Collections.Generic;
using System.IO;
using Horn.Core.BuildEngines;
using Horn.Core.PackageStructure;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.Dependencies
{
    public class when_dispatching_dependencies : dependency_dispatcher_context
    {

        protected override void Because()
        {
            dispatcher.Dispatch(packageTree, dependencies, dependencyPath);
        }


        [Fact]
        public void should_move_dependencies()
        {
            FileInfo[] files = new DirectoryInfo(dependencyPath).GetFiles("*.dll");

            Assert.Equal(1, files.Length);
        }
        [Fact]
        public void should_delegate_to_dependant_updater()
        {
            dependentUpdater.AssertWasCalled(x => x.Execute(Arg<IPackageTree>.Is.Anything, Arg<IEnumerable<string>>.Is.Anything, Arg<Dependency>.Is.Anything));
        }

    }
}