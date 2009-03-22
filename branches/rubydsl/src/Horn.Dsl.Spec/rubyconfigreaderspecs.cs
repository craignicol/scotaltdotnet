using System;
using System.IO;
using Horn.Core.Dsl;
using Horn.Dsl.Spec.Helpers;
using Xunit;

namespace Horn.Dsl.Spec.RubyConfigReader
{
    public class When_a_ruby_build_file_is_retrieved : Specification
    {
        private IBuildConfigReader rubyConfigReader;

        private IBuildMetaData buildMetaData;

        protected override void Before_each_spec()
        {
            rubyConfigReader = new RubyBuildConfigReader();
        }


        protected override void Because()
        {
            var baseDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            buildMetaData = rubyConfigReader.SetDslFactory(baseDirectory).GetBuildMetaData(baseDirectory, "whole_example");
        }

        [Fact]
        public void Then_the_build_meta_data_is_parsed()
        {
            DlrHelper.AssertBuildMetaData(buildMetaData);
        }
    }
}