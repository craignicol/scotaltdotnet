using System;
using System.IO;
using Horn.Domain.Exceptions;
using Horn.Domain.PackageStructure;
using Rhino.DSL;

namespace Horn.Domain.Dsl
{
    public class BooBuildConfigReader : IBuildConfigReader
    {

        protected DslFactory factory;
        private BooConfigReader configReader;


        public IPackageTree PackageTree{ get; private set; }



        public BuildMetaData GetBuildMetaData(string packageName)
        {
            if (factory == null)
                throw new ArgumentNullException("You have not called SetDslFactory on class BooBuildConfigReader");

            return CreateBuildMetaData(PackageTree.CurrentDirectory, packageName);
        }

        public BuildMetaData GetBuildMetaData(IPackageTree packageTree, string buildFile)
        {
            if (factory == null)
                throw new ArgumentNullException("You have not called SetDslFactory on class BooBuildConfigReader");

            return CreateBuildMetaData(packageTree.CurrentDirectory, buildFile);
        }

        public virtual IBuildConfigReader SetDslFactory(IPackageTree packageTree)
        {
            PackageTree = packageTree;

            factory = new DslFactory
                            {
                                BaseDirectory = packageTree.CurrentDirectory.FullName
                            };

            factory.Register<BooConfigReader>(new ConfigReaderEngine());

            return this;
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
                throw new MissingBuildFileException(buildFolder, e);
            }
          
            configReader.Prepare();

            //return new BuildMetaData(configReader);
            return null;
        }



    }
}