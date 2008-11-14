using System.Collections.Generic;
using System.IO;
using Horn.Core.dsl;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    public abstract class BaseDSLSpecification : Specification
    {
        protected const string DESCRIPTION = "This is a description of horn";

        protected const string SVN_URL = "https://svnserver/trunk";

        protected const string BUILD_FILE = "rakefile.rb";

        protected readonly List<string> TASKS = new List<string> {"build", "test", "deploy"};

        protected DirectoryInfo rootDirectory;
        protected IBuildConfigReader reader;

        protected BaseConfigReader GetConfigReaderInstance()
        {
            BaseConfigReader ret = new ConfigReaderDouble();

            ret.description(DESCRIPTION);
            ret.BuildEngine = new RakeBuildEngine(BUILD_FILE);
            ret.SourceControl = new SVNSourceControl(SVN_URL);

            ret.BuildEngine.AssignTasks(TASKS.ToArray());

            return ret;
        }

        public static void AssertBuildMetaDataValues(BuildMetaData metaData)
        {
            Assert.IsAssignableFrom(typeof(SVNSourceControl), metaData.SourceControl);

            Assert.Equal(SVN_URL, metaData.SourceControl.Url);

            Assert.IsAssignableFrom(typeof (RakeBuildEngine), metaData.BuildEngine);

            Assert.Equal(BUILD_FILE, metaData.BuildEngine.BuildFile);

            Assert.Equal(3, metaData.BuildEngine.Tasks.Count);
        }
    }
}