using System.Collections.Generic;
using Horn.Core.PackageCommands;
using Xunit;

namespace Horn.Core.Spec.Unit.PackageCommands
{
    public class When_The_Builder_Receives_An_Install_Switch : PackageBuilderSpecificationBase
    {
        protected override void Because()
        {
            switches.Add("install", new List<string>{"horn"});
        }

        [Fact]
        public void Then_The_Builder_Coordinates_The_Build()
        {
            IPackageCommand command = new PackageBuilder();

            command.Execute(switches);
        }
    }
}