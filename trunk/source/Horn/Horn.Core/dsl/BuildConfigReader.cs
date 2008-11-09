using System;
using Rhino.DSL;

namespace Horn.Core.dsl
{
    public class BuildConfigReader : IBuildConfigReader
    {
        private readonly DslFactory factory;

        private BaseConfigReader configReader;

        public BuildMetaData GetBuildMetaData(string sourceName)
        {
            if(string.IsNullOrEmpty(sourceName))
                throw new UnknownBuildComponentException("Empty build component passed into the BuildConfigReader");

            var buildFile = GetBuildFile(sourceName);

            try
            {
                configReader = factory.Create<BaseConfigReader>(buildFile);
            }
            catch (InvalidOperationException e)
            {
                throw new UnknownBuildComponentException(string.Format("Unkown build component {0} passed into the BuildConfigReader", buildFile), e);
            }

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