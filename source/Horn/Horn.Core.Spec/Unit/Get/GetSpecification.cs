namespace Horn.Core.Spec.Unit.Get
{
    using Core.Get;
    using Rhino.Mocks;
    using Xunit;

    public class When_Asked_To_Get_Source : GetSpecificationBase
    {
        private string destinationPath;

        protected override void Because()
        {
            get = new Get(fileSystemProvider);
            destinationPath = get.Project(project).From(sourceControl).Export();
        }

        [Fact]
        public void Should_Create_Directory()
        {
            fileSystemProvider.AssertWasCalled(r => r.CreateDirectory(Arg<string>.Is.Anything));
        }

        [Fact]
        public void Should_Retrieve_Source_From_VersionControl()
        {
            Assert.True(sourceControl.ExportWasCalled);
        }

        [Fact]
        public void Should_Return_The_Destination_Path()
        {
            Assert.NotEqual(string.Empty, destinationPath);
        }
    }
}