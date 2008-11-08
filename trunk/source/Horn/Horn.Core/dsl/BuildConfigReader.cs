using System;
using Rhino.DSL;

namespace Horn.Core.dsl
{
    public class BuildConfigReader : IBuildConfigReader
    {
        private readonly DslFactory factory;

        public BuildMetaData GetBuildMetaData(string sourceName)
        {
            var buildFile = GetBuildFile(sourceName);

            var configReader = factory.Create<BaseConfigReader>(buildFile);

            configReader.Prepare();

            return new BuildMetaData(configReader);
        }

        protected virtual string GetBuildFile(string sourceName)
        {
            return string.Format("BuildConfigs/{0}.boo", sourceName);
        }

        public BuildConfigReader()
        {
            factory = new DslFactory
                          {
                              BaseDirectory = AppDomain.CurrentDomain.BaseDirectory
                          };

            factory.Register<BaseConfigReader>(new ConfigReaderEngine());
        }
    }
}