using System.Collections.Generic;
using System.IO;
using Horn.Core.Dsl;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;
using Horn.Core.Utils.Framework;
using Horn.Framework.helpers;
using Horn.Spec.Framework.Extensions;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    using Core.Dependencies;
    using extensions;

    public abstract class BaseDSLSpecification : Specification
    {
        protected const string DESCRIPTION = "A .NET build and dependency manager";

        protected const string SVN_URL = "http://scotaltdotnet.googlecode.com/svn/trunk/";

        protected const string FILE_NAME = "horn";

        protected const string BUILD_FILE = "src/horn.sln";

        public static readonly Dictionary<string, object> METADATA = new Dictionary<string, object> { { "homepage", "http://code.google.com/p/scotaltdotnet/" }, { "forum", "http://groups.google.co.uk/group/horn-development?hl=en" }, { "contrib", false} };

        public  static readonly List<string> TASKS = new List<string> {"build"};

        public const string OUTPUT_DIRECTORY = "Output";

        protected DirectoryInfo rootDirectory;

        protected IPackageTree packageTree;

        protected IBuildConfigReader reader;

        public static BooConfigReader GetConfigReaderInstance()
        {
            BooConfigReader ret = new ConfigReaderDouble();

            ret.description(DESCRIPTION);
            ret.BuildEngine = new BuildEngines.BuildEngine(new MSBuildBuildTool(), BUILD_FILE, FrameworkVersion.FrameworkVersion35, CreateStub<IDependencyDispatcher>());
            ret.SourceControl = new SVNSourceControl(SVN_URL);
            ret.PackageMetaData.PackageInfo = METADATA;
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
            
            Assert.Equal(METADATA.Count, metaData.ProjectInfo.Count);

            METADATA.ForEach(x => Assert.Contains(x, metaData.ProjectInfo));

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