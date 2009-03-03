using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.rb");
        }

        [Fact]
        public void Then_a_build_metadata_object_is_returned()
        {                       
            var engine = Ruby.CreateEngine();

            engine.Runtime.LoadAssembly(typeof (BuildMetaData).Assembly);

            engine.ExecuteFile(buildFile);

            var klass = engine.Runtime.Globals.GetVariable("MetaDataFactory");

            var instance = (RubyObject)engine.Operations.CreateInstance(klass);

            var metaData = (BuildMetaData)engine.Operations.InvokeMember(instance, "return_meta_data");

            Assert.Equal(metaData.Description, "A description of sorts");

            Assert.Equal(metaData.Dependencies.Count, 1);
        }
    }
}
