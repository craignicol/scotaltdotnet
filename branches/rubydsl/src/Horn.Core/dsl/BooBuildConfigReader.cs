using System;
using System.IO;
using Horn.Core.PackageStructure;
using Rhino.DSL;

namespace Horn.Core.Dsl
{
    public class BooBuildConfigReader : IBuildConfigReader
    {

        protected DslFactory factory;
        private BooConfigReader configReader;


        public DirectoryInfo PackageDirectory { get; private set; }



        public BuildMetaData GetBuildMetaData(string packageName)
        {
            if (factory == null)
                throw new ArgumentNullException("You have not called SetDslFactory on class BooBuildConfigReader");

            return CreateBuildMetaData(PackageDirectory, packageName);
        }

        public BuildMetaData GetBuildMetaData(DirectoryInfo buildFolder, string buildFile)
        {
            if (factory == null)
                throw new ArgumentNullException("You have not called SetDslFactory on class BooBuildConfigReader");

            return CreateBuildMetaData(buildFolder, buildFile);
        }

        private BuildMetaData CreateBuildMetaData(DirectoryInfo buildFolder, string buildFile)
        {
            var buildFileResolver = new BuildFileResolver();

            var buildFilePath = buildFileResolver.Resolve(buildFolder, buildFile).BuildFile;

            try
            {
                configReader = factory.Create<BooConfigReader>(buildFilePath);
            }
            catch (InvalidOperationException e)
            {
                throw new MissingBuildFileException(PackageDirectory, e);
            }
          
            configReader.Prepare();

            return new BuildMetaData(configReader);
        }

        public virtual IBuildConfigReader SetDslFactory(DirectoryInfo baseDirectory)
        {
            PackageDirectory = baseDirectory;

            factory = new DslFactory
                            {
                                BaseDirectory = baseDirectory.FullName
                            };

            factory.Register<BooConfigReader>(new ConfigReaderEngine());

            return this;
        }



    }
}