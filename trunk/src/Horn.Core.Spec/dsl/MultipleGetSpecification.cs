using System;
using System.IO;
using Horn.Core.Dsl;
using Horn.Core.SCM;
using Horn.Framework.helpers;
using Rhino.DSL;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    public class When_we_need_record_scm_export_data : Specification
    {
        private const string Url = "http://scotaltdotnet.googlecode.com/svn/trunk/src/one";

        private const string ExportToPath = @"C:\exportto";

        private ExportData exportData;

        protected override void Because()
        {
            exportData = new ExportData(Url, ExportToPath, SourceControlType.Svn);
        }

        [Fact]
        public void Then_the_model_can_express_this()
        {
            Assert.Equal(Url, exportData.Url);
            Assert.Equal(SourceControlType.Svn, exportData.SourceControlType);
            Assert.Equal(ExportToPath, exportData.To.FullName);
        }
    }

    public class When_the_build_file_contains_mulitple_export_steps : BaseDSLSpecification
    {
        private BooConfigReader configReader;

        protected DslFactory factory;
        private IDependencyResolver dependencyResolver;

        protected override void Before_each_spec()
        {
            dependencyResolver = CreateStub<IDependencyResolver>();
            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>())
                .Return(new SVNSourceControl(string.Empty));

            IoC.InitializeWith(dependencyResolver);

            var engine = new ConfigReaderEngine();

            factory = new DslFactory { BaseDirectory = DirectoryHelper.GetBaseDirectory() };
            factory.Register<BooConfigReader>(engine);
        }

        protected override void After_each_spec()
        {
            IoC.InitializeWith(null);
        }

        protected override void Because()
        {
            configReader = factory.Create<BooConfigReader>(@"BuildConfigs/Horn/hornmultiget.boo");
            configReader.Prepare();
        }

        [Fact]
        public void Then_the_model_contains_multiple_()
        {
            Assert.Equal("http://scotaltdotnet.googlecode.com/svn/trunk/src/one", configReader.ExportList[0].Url);
            Assert.Equal("http://scotaltdotnet.googlecode.com/svn/trunk/src/two", configReader.ExportList[1].Url);
            Assert.Equal(SourceControlType.Svn, configReader.ExportList[0].SourceControlType);
            Assert.Equal(SourceControlType.Svn, configReader.ExportList[1].SourceControlType);
        }
    }
}