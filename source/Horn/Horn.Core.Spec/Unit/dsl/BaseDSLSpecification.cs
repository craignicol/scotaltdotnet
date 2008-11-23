using System.Collections.Generic;
using System.IO;
using Horn.Core.dsl;
using Horn.Core.SCM;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    public abstract class BaseDSLSpecification : Specification
    {
        protected const string DESCRIPTION = "This is a description of horn";

        protected const string SVN_URL = "https://svnserver/trunk";

        protected const string BUILD_FILE = "Horn.build";

        public  static readonly List<string> TASKS = new List<string> {"build"};

        protected DirectoryInfo rootDirectory;
        protected IBuildConfigReader reader;

        public static BaseConfigReader GetConfigReaderInstance()
        {
            BaseConfigReader ret = new ConfigReaderDouble();

            ret.description(DESCRIPTION);
            ret.BuildEngine = new Core.BuildEngine(new NAntBuildTool(), BUILD_FILE);
            ret.SourceControl = new SVNSourceControl(SVN_URL);

            ret.BuildEngine.AssignTasks(TASKS.ToArray());

            return ret;
        }

        public static void AssertBuildMetaDataValues(IBuildMetaData metaData)
        {
            Assert.IsAssignableFrom<SVNSourceControl>(metaData.SourceControl);

            Assert.Equal(SVN_URL, metaData.SourceControl.Url);

            Assert.IsAssignableFrom<NAntBuildTool>(metaData.BuildEngine.BuildTool);

            Assert.Equal(BUILD_FILE, metaData.BuildEngine.BuildFile);

            Assert.Equal(1, metaData.BuildEngine.Tasks.Count);
        }
    }
}