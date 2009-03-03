using System;
using System.IO;
using Horn.Core.DSL.Domain;
using IronRuby;
using IronRuby.Builtins;
using Xunit;

namespace Horn.Dsl.Specific
{
    public class When_creating_a_clr_object_in_ironruby : Specification
    {
        private string buildFile;

        protected override void Because()
        {
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.rb");
        }

        [Fact]
        public void Then_the_object_should_be_accessible_in_csharp()
        {                       
            var engine = Ruby.CreateEngine();

            engine.Runtime.LoadAssembly(typeof (BuildMetaData).Assembly);

            engine.ExecuteFile(buildFile);

            var klass = engine.Runtime.Globals.GetVariable("MetaDataFactory");

            var instance = (RubyObject)engine.Operations.CreateInstance(klass);


            //You must have shadow-copying turned off for the next line to run and for the test to pass.
            //E.g. in R# go to "ReSharper/Options/Unit Testing/Shadow-Copy Assemblies being tested" should be un-checked.
            var metaData = (BuildMetaData)engine.Operations.InvokeMember(instance, "return_meta_data");

            Assert.Equal(metaData.Description, "A description of sorts");

            Assert.Equal(metaData.Dependencies.Count, 1);
        }
    }
}
