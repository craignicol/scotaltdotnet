using System.Collections.Generic;
using System.IO;
using Horn.Domain.Dsl;
using Horn.Domain.Framework;
using Horn.Domain.PackageStructure;
using Horn.Domain.SCM;
using Horn.Domain.Utils;
using Horn.Framework.helpers;
using Horn.Spec.Framework.Extensions;
using Xunit;

namespace Horn.Domain.Spec.Unit.dsl
{
    public abstract class BaseDSLSpecification : Specification
    {
        protected const string DESCRIPTION = "A .NET build and dependency manager";

        protected const string SVN_URL = "http://scotaltdotnet.googlecode.com/svn/trunk/";

        protected const string FILE_NAME = "horn";

        protected const string BUILD_FILE = "src/horn.sln";

        public static readonly Dictionary<string, string> METADATA = new Dictionary<string, string> { { "contrib", "false" }, { "createdate", "24/01/2009" }, { "France","yuky" } };

        public  static readonly List<string> TASKS = new List<string> {"build"};

        public const string OUTPUT_DIRECTORY = "Output";

        protected DirectoryInfo rootDirectory;

        protected IPackageTree packageTree;

        protected IBuildConfigReader reader;

        public static BooConfigReader GetConfigReaderInstance()
        {
            BooConfigReader ret = new ConfigReaderDouble();

            ret.description(DESCRIPTION);
            ret.BuildEngine = new BuildEngines.BuildEngine(new MSBuildBuildTool(), BUILD_FILE, FrameworkVersion.FrameworkVersion35);
            ret.SourceControl = new SVNSourceControl(SVN_URL);
            ret.BuildEngine.MetaData = METADATA;
            ret.BuildEngine.AssignTasks(TASKS.ToArray());
            ret.BuildEngine.OutputDirectory = OUTPUT_DIRECTORY;
            ret.BuildEngine.SharedLibrary = ".";
            ret.BuildEngine.GenerateStrongKey = true;

            return ret;
        }

        public static void AssertBuildMetaDataValues(IBuildMetaData metaData)
        {
            Assert.IsAssignableFrom<SVNSourceControl>(metaData.SourceControl);

            Assert.Equal(SVN_URL, metaData.SourceControl.Url);

            Assert.IsAssignableFrom<MSBuildBuildTool>(metaData.BuildEngine.BuildTool);
            
            Assert.Equal(METADATA.Count, metaData.BuildEngine.MetaData.Count);
            METADATA.ForEach(x => Assert.Contains(x, metaData.BuildEngine.MetaData));

            Assert.Equal(BUILD_FILE, metaData.BuildEngine.BuildFile);

            Assert.Equal(OUTPUT_DIRECTORY, metaData.BuildEngine.OutputDirectory);

            Assert.Equal(".", metaData.BuildEngine.SharedLibrary);
        }

        protected DirectoryInfo GetTestBuildConfigsFolder()
        {
            return new DirectoryInfo(string.Format("{0}\\BuildConfigs\\Horn", DirectoryHelper.GetBaseDirectory().ToLower().ResolvePath()));
        }
    }
}
