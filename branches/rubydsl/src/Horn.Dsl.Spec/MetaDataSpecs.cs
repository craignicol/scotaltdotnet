using System;
using System.IO;
using Horn.Core.Dsl;
using Horn.Core.SCM;
using Horn.Dsl.Spec.Helpers;
using IronRuby;
using Xunit;

namespace Horn.Dsl.Spec
{
    public class When_an_install_of_horn_with_a_description_is_given : Specification
    {
        private string buildFile;

        private BuildMetaData buildMetaData;

        protected override void Before_each_spec()
        {
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "description_example.rb");
        }

        protected override void Because()
        {
            buildMetaData = DlrHelper.RetrieveBuildMetaDataFromTheDlr(buildFile, "ClrAccessor", "get_build_metadata");
        }
         
        [Fact]
        public void Then_the_description_can_be_retrived_from_the_metatdata_file()
        {
            Assert.Equal("A .NET build and dependency manager", buildMetaData.Description);
        }
    }

    public class When_the_horn_file_contains_an_instruction_to_get_from_svn : Specification
    {
        private string buildFile;

        private BuildMetaData buildMetaData;

        protected override void Before_each_spec()
        {
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "get_from_svn_example.rb");
        }

        protected override void Because()
        {
            buildMetaData = DlrHelper.RetrieveBuildMetaDataFromTheDlr(buildFile, "ClrAccessor", "get_build_metadata");
        }

        [Fact]
        public void Then_the_meta_data_contains_a_svn_source_control_reference()
        {
            Assert.IsAssignableFrom<SVNSourceControl>(buildMetaData.SourceControl);
        }
    }
}