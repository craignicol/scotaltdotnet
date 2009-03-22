using System.IO;
using Horn.Core.PackageStructure;
using IronRuby;

namespace Horn.Core.Dsl
{
    public class RubyBuildConfigReader : IBuildConfigReader
    {

        public DirectoryInfo PackageDirectory { get; private set; }



        public BuildMetaData GetBuildMetaData(string packageName)
        {
            var buildFileResolver = new BuildFileResolver();

            var buildFilePath = buildFileResolver.Resolve(PackageDirectory, packageName).BuildFile;

            return CreateBuildMetaData(buildFilePath);
        }

        public BuildMetaData GetBuildMetaData(DirectoryInfo buildFolder, string buildFile)
        {
            var buildFileResolver = new BuildFileResolver();

            var buildFilePath = buildFileResolver.Resolve(buildFolder, buildFile).BuildFile;

            return CreateBuildMetaData(buildFilePath);
        }

        public IBuildConfigReader SetDslFactory(DirectoryInfo rootDirectory)
        {
            PackageDirectory = rootDirectory;

            return this;
        }



        private BuildMetaData CreateBuildMetaData(string buildFilePath)
        {
            var engine = Ruby.CreateEngine();

            engine.Runtime.LoadAssembly(typeof(BuildMetaData).Assembly);

            engine.ExecuteFile(buildFilePath);

            var klass = engine.Runtime.Globals.GetVariable("ClrAccessor");

            var instance = engine.Operations.CreateInstance(klass);

            return (BuildMetaData)engine.Operations.InvokeMember(instance, "get_build_metadata");
        }



    }
}