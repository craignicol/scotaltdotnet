using System;
using System.IO;
using IronRuby;
using Microsoft.Scripting.Hosting;
using Xunit;

namespace Horn.Dsl.Specificatioin
{
    public class When_given_a_dsl_file_path : Specification
    {
        private string buildFile;

        protected override void Because()
        {
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "build.rb");
        }

        [Fact]
        public void Then_the_dsl_is_parsed()
        {            
            
            var engine = Ruby.CreateEngine();

            engine.ExecuteFile(buildFile);

            var code = String.Format("{0}.new.method :{1}", "Builder", "do_it");

            var action = engine.CreateScriptSourceFromString(code).Execute();

            var result = engine.Operations.Call(action);
        }
    }
}
