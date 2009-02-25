using System;
using System.IO;
using Horn.Core.DSL.Domain;
using IronRuby;
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

            var code = String.Format("{0}.new.method :{1}", "MetaDataFactory", "return_meta_data");

            var action = engine.CreateScriptSourceFromString(code).Execute();

            var result = (BuildMetaData)engine.Operations.Call(action);

            Assert.Equal(result.Description, "A description of sorts");
        }
    }
}
