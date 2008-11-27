using Xunit;
namespace Horn.Core.Spec.Unit.Get
{
    using GetOperations;

    public class When_Get_Is_Request_To_Retrieve_Source : GetSpecificationBase
    {
        private string destinationPath;

        protected override void Because()
        {
            get = new Get(fileSystemProvider);

            destinationPath = get.Package(package)
                                .From(sourceControl)
                                .ExportTo(packageTree
                                .Retrieve("horn").WorkingDirectory.FullName);
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