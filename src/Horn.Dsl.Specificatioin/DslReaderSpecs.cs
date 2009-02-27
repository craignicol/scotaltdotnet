using System;
using System.IO;
using Horn.Core.DSL.Domain;
using IronRuby;
using IronRuby.Builtins;
using Xunit;

namespace Horn.Dsl.Specificatioin
{
    public class When_parsing_a_build_file : Specification
    {
        private string buildFile;

        protected override void Because()
        {
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "build.rb");
        }

        [Fact]
        public void Then_a_build_metadata_object_is_returned()
        {                       
            var engine = Ruby.CreateEngine();

            engine.ExecuteFile(buildFile);

            var klass = engine.Runtime.Globals.GetVariable("MetaDataFactory");

            var instance = (RubyObject)engine.Operations.CreateInstance(klass);

            var result = (Horn.Core.DSL.Domain.BuildMetaData)engine.Operations.InvokeMember(instance, "return_meta_data");

            //Assert.Equal(result.Description, "A description of sorts");
        }
    }
}
