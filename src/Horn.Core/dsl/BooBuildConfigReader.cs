using System;
using System.IO;
using Rhino.DSL;

namespace Horn.Core.Dsl
{
    public class BooBuildConfigReader : IBuildConfigReader
    {
        protected DslFactory factory;

        private BooConfigReader configReader;

        private const string BUILD_FILE_NAME = "build.boo";

        public DirectoryInfo PackageDirectory { get; private set; }

        public BuildMetaData GetBuildMetaData()
        {
            if (factory == null)
                throw new ArgumentNullException("You have not called SetDslFactory on class BooBuildConfigReader");

            try
            {
                configReader = factory.Create<BooConfigReader>(BUILD_FILE_NAME);
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