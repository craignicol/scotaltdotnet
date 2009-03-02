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
            buildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "build.rb");
        }

        [Fact]
        public void Then_a_build_metadata_object_is_returned()
        {                       
            var engine = Ruby.CreateEngine();

            engine.ExecuteFile(buildFile);

            var klass = engine.Runtime.Globals.GetVariable("MetaDataFactory");

            var instance = (RubyObject)engine.Operations.CreateInstance(klass);

            var return_meta_data = engine.Operations.InvokeMember(instance, "return_meta_data");

            var description = return_meta_data.GetType().GetProperty("Description", typeof(string)).GetValue(return_meta_data, null);

            var dependencies = return_meta_data.GetType().GetProperties()[1].GetValue(return_meta_data, null);

            Assert.Equal(description, "A description of sorts");

            Assert.Equal(((List<Dependency>) dependencies).Count, 1);
        }
    }
}
