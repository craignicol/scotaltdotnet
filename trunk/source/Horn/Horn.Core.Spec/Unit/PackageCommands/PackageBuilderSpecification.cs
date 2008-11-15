using System.Collections.Generic;
using Horn.Core.dsl;
using Horn.Core.Get;
using Horn.Core.PackageCommands;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.Unit.PackageCommands
{
    public class When_The_Builder_Receives_An_Install_Switch : PackageCommandSpecificationBase
    {
        protected override void Because()
        {
            switches.Add("install", new List<string>{"horn"});

            get = CreateStub<IGet>();

            buildConfigReader = CreateStub<IBuildConfigReader>();
        }

        [Fact]
        public void Then_The_Builder_Coordinates_The_Build()
        {
            IPackageCommand command = new PackageBuilder(get);

            command.Execute(packageTree, switches);
        }
    }
}