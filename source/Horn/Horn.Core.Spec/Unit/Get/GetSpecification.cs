namespace Horn.Core.Spec.Unit.Get
{
    using Core.Get;
    using Rhino.Mocks;
    using Xunit;

    public class When_Asked_To_Get_Source : GetSpecificationBase
    {
        protected override void Because()
        {
            get = new Get(fileSystemProvider);
            get.Project(project).From(versionControl).Export();
        }

        [Fact]
        public void Should_Create_Directory()
        {
            fileSystemProvider.AssertWasCalled(r => r.CreateDirectory(Arg<string>.Is.Anything));
        }

        [Fact]
        public void Should_Ask_Project_For_The_VersionControl_Settings()
        {
            project.AssertWasCalled(p => p.GetVersionControlParameters());
        }

        [Fact]
        public void Should_Retrieve_Source_From_VersionControl()
        {
            versionControl.AssertWasCalled(s => s.Export(Arg<VersionControlParameters>.Is.Anything));
        }
    }
}