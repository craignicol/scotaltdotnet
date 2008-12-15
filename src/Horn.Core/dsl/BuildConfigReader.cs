using System;
using System.IO;
using Rhino.DSL;

namespace Horn.Core.dsl
{
    public class BuildConfigReader : IBuildConfigReader
    {
        protected DslFactory factory;

        private BaseConfigReader configReader;

        private const string BUILD_FILE_NAME = "build.boo";

        public DirectoryInfo PackageDirectory { get; private set; }

        public BuildMetaData GetBuildMetaData()
        {
            if (factory == null)
                throw new ArgumentNullException("You have not called SetDslFactory on class BuildConfigReader");

            try
            {
                configReader = factory.Create<BaseConfigReader>(BUILD_FILE_NAME);
            }
            catch (InvalidOperationException e)
            {
                throw new MissingBuildFileException(string.Format("No build.boo file component {0} at path {1}.", PackageDirectory.Name, PackageDirectory.FullName), e);
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

            factory.Register<BaseConfigReader>(new ConfigReaderEngine());

            return this;
        }
    }
}