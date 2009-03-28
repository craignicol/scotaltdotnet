using Horn.Core.PackageStructure;
using IronRuby;

namespace Horn.Core.Dsl
{
    public class RubyBuildConfigReader : IBuildConfigReader
    {

        public IPackageTree PackageTree { get; private set; }



        public BuildMetaData GetBuildMetaData(string packageName)
        {
            var buildFileResolver = new BuildFileResolver();

            var buildFilePath = buildFileResolver.Resolve(PackageTree.CurrentDirectory, packageName).BuildFile;

            return CreateBuildMetaData(buildFilePath);
        }

        public BuildMetaData GetBuildMetaData(IPackageTree packageTree, string buildFile)
        {
            var buildFileResolver = new BuildFileResolver();

            var buildFilePath = buildFileResolver.Resolve(packageTree.CurrentDirectory, buildFile).BuildFile;

            return CreateBuildMetaData(buildFilePath);
        }

        public IBuildConfigReader SetDslFactory(IPackageTree packageTree)
        {
            PackageTree = packageTree;

            return this;
        }



        private BuildMetaData CreateBuildMetaData(string buildFilePath)
        {
            var engine = Ruby.CreateEngine();

            var context = Ruby.GetExecutionContext(engine);

            context.Loader.SetLoadPaths(new[] { PackageTree.RetrievePackage("rubylib").CurrentDirectory.FullName });

            engine.Runtime.LoadAssembly(typeof(BuildMetaData).Assembly);

            engine.ExecuteFile(buildFilePath);

            var klass = engine.Runtime.Globals.GetVariable("ClrAccessor");

            var instance = engine.Operations.CreateInstance(klass);

            return (BuildMetaData)engine.Operations.InvokeMember(instance, "get_build_metadata");
        }



    }
}