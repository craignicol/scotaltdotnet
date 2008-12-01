using System.Collections.Generic;
using System.IO;
using Horn.Core.dsl;
using Horn.Core.SCM;
using Horn.Core.Utils.Framework;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    public abstract class BaseDSLSpecification : Specification
    {
        protected const string DESCRIPTION = "This is a description of horn";

        protected const string SVN_URL = "https://scotaltdotnet.googlecode.com/svn/trunk/";

        protected const string BUILD_FILE = "horn.sln";

        public  static readonly List<string> TASKS = new List<string> {"build"};

        protected DirectoryInfo rootDirectory;

        protected IBuildConfigReader reader;

        public static BaseConfigReader GetConfigReaderInstance()
        {
            BaseConfigReader ret = new ConfigReaderDouble();

            ret.description(DESCRIPTION);
            ret.BuildEngine = new Core.BuildEngine(new MSBuildBuildTool(), BUILD_FILE, FrameworkVersion.frameworkVersion35);
            ret.SourceControl = new SVNSourceControl(SVN_URL);

            ret.BuildEngine.AssignTasks(TASKS.ToArray());

            return ret;
        }

        public static void AssertBuildMetaDataValues(IBuildMetaData metaData)
        {
            Assert.IsAssignableFrom<SVNSourceControl>(metaData.SourceControl);

            Assert.Equal(SVN_URL, metaData.SourceControl.Url);

            Assert.IsAssignableFrom<MSBuildBuildTool>(metaData.BuildEngine.BuildTool);

            Assert.Equal(BUILD_FILE, metaData.BuildEngine.BuildFile);
        }
    }
}