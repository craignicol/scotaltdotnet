using System;
using System.IO;
using Horn.Core;
using Horn.Core.Dsl;
using Horn.Core.SCM;
using Horn.Core.Utils.Framework;
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

    public class When_the_metadata_specifies_which_build_engine_to_build_with : Specification
    {
        private string buildFile;

        private BuildMetaData buildMetaData;

        protected override void Before_each_spec()
        {
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "build_with_example.rb");
        }

        protected override void Because()
        {
            buildMetaData = DlrHelper.RetrieveBuildMetaDataFromTheDlr(buildFile, "ClrAccessor", "get_build_metadata");
        }

        [Fact]
        public void Then_the_meta_data_contains_a_svn_source_control_reference()
        {
            Assert.Equal("src/horn.sln", buildMetaData.BuildEngine.BuildFile);

            Assert.Equal(FrameworkVersion.FrameworkVersion35, buildMetaData.BuildEngine.Version);

            Assert.IsAssignableFrom<MSBuildBuildTool>(buildMetaData.BuildEngine.BuildTool);
        }
    }

    public class When_the_metadata_specifies_project_info : Specification
    {
        private string buildFile;

        private BuildMetaData buildMetaData;

        protected override void Before_each_spec()
        {
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "meta_data_example.rb");
        }

        protected override void Because()
        {
            buildMetaData = DlrHelper.RetrieveBuildMetaDataFromTheDlr(buildFile, "ClrAccessor", "get_build_metadata");
        }

        [Fact]
        public void Then_the_project_info_will_contain_home_page_details()
        {
            Assert.Equal(buildMetaData.ProjectInfo["homepage"].ToString(), "http://code.google.com/p/scotaltdotnet/");
        }
    }

    public class When_the_metadata_specifies_project_dependencies : Specification
    {
        private string buildFile;

        private BuildMetaData buildMetaData;

        protected override void Before_each_spec()
        {
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dependencies_example.rb");
        }

        protected override void Because()
        {
            buildMetaData = DlrHelper.RetrieveBuildMetaDataFromTheDlr(buildFile, "ClrAccessor", "get_build_metadata");
        }

        [Fact]
        public void Then_the_dependencies_can_be_retrieved()
        {
            Assert.True(buildMetaData.BuildEngine.Dependencies.Count > 0);
        }
    }

    public class When_given_all_the_meta_data_for_a_project : Specification
    {
        private string buildFile;

        private BuildMetaData buildMetaData;

        protected override void Before_each_spec()
        {
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "whole_example.rb");
        }

        protected override void Because()
        {
            buildMetaData = DlrHelper.RetrieveBuildMetaDataFromTheDlr(buildFile, "ClrAccessor", "get_build_metadata");
        }

        [Fact]
        public void Then_the_whole_metat_data_can_be_obtained()
        {
            DlrHelper.AssertBuildMetaData(buildMetaData);
        }
    }
}