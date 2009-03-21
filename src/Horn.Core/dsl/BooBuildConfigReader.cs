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

        public BuildMetaData GetBuildMetaData(string fileName)
        {
            if (factory == null)
                throw new ArgumentNullException("You have not called SetDslFactory on class BooBuildConfigReader");

            var buildFileResolver = new BuildFileResolver();

            var buildFile = buildFileResolver.Resolve(PackageDirectory, fileName).BuildFile;

            try
            {
                configReader = factory.Create<BooConfigReader>(buildFile);
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