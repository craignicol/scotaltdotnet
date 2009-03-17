using System;
using System.IO;
using Horn.Core.Dsl;
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
            var engine = Ruby.CreateEngine();

            engine.Runtime.LoadAssembly(typeof(BuildMetaData).Assembly);

            engine.ExecuteFile(buildFile);

            var klass = engine.Runtime.Globals.GetVariable("ClrAccessor");

            var instance = engine.Operations.CreateInstance(klass);

            buildMetaData = (BuildMetaData)engine.Operations.InvokeMember(instance, "get_build_metadata");
        }

        [Fact]
        public void Then_the_meta_data_object_should_be_created()
        {
            Assert.Equal("A .NET build and dependency manager", buildMetaData.Description);
        }
    }
}