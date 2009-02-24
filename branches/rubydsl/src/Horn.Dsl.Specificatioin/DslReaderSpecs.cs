using System;
using System.IO;
using IronRuby;
using Microsoft.Scripting.Hosting;
using Xunit;

namespace Horn.Dsl.Specificatioin
{
    public class When_requesting_a_string_value_from_iron_ruby : Specification
    {
        private string buildFile;

        protected override void Because()
        {
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "build.rb");
        }

        [Fact]
        public void Then_a_string_type_is_returned()
        {                       
            var engine = Ruby.CreateEngine();

            var scope = Ruby.CreateRuntime().CreateScope();
            engine.ExecuteFile(buildFile);

            var code = String.Format("{0}.new.method :{1}", "SimpleType", "return_string");

            var action = engine.CreateScriptSourceFromString(code).Execute();

            var result = engine.Operations.Call(action);

            Assert.Equal(result.ToString(), "This should be returned");
        }
    }
}
