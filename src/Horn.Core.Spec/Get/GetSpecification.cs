using Xunit;
namespace Horn.Domain.Spec.Unit.Get
{
    public class When_a_get_request_is_made_to_retrieve_the_source : GetSpecificationBase
    {
        private string destinationPath;

        protected override void Because()
        {
            get = new Core.GetOperations.Get(fileSystemProvider);

            destinationPath = get.Package(package)
                                .From(sourceControl)
                                .ExportTo(packageTree)
                                .RetrievePackage("horn").WorkingDirectory.FullName;
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