namespace Horn.Core.Spec.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using BuildEngines;
    using Core.Dependencies;
    using Dsl;
    using PackageStructure;

    public abstract class dependency_dispatcher_context : Specification
    {
        protected DependencyDispatcher dispatcher;
        protected IPackageTree packageTree;
        protected string targetDirectory;
        private string outputPath;
        protected string workingPath;
        protected string dependencyPath;
        protected Dependency[] dependencies;
        protected IDependentUpdaterExecutor dependentUpdater;

        protected override void Before_each_spec()
        { 
            targetDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid().ToString());
            packageTree = CreateStub<FakePackageTree>(new[] { targetDirectory });
            dependentUpdater = CreateStub<IDependentUpdaterExecutor>();

            dependencies = new[] { new Dependency("Test", "Test.Dependency"), };

            outputPath = packageTree.OutputDirectory.FullName;
            workingPath = packageTree.WorkingDirectory.FullName;
            dependencyPath = Path.Combine(workingPath, "dependencies");

            CreateDirectories();
            CreateFiles();

            dispatcher = new DependencyDispatcher(dependentUpdater);
        }

        private void CreateFiles()
        {
            foreach (var dependency in dependencies)
            {
                File.WriteAllText(Path.Combine(outputPath, dependency.Library + ".dll"), "This is a fake dependency");
                File.WriteAllText(Path.Combine(dependencyPath, dependency.Library + ".dll"), "This is a fake dependency that will be replaced");
            }
        }

        private void CreateDirectories()
        {
            Directory.CreateDirectory(targetDirectory);
            Directory.CreateDirectory(outputPath);
            Directory.CreateDirectory(workingPath);
            Directory.CreateDirectory(dependencyPath);
        }

        protected override void After_each_spec()
        {
            //if (Directory.Exists(targetDirectory))
            //    Directory.Delete(targetDirectory, true);
        }
    }

    public class FakePackageTree : IPackageTree
    {
        private readonly string baseDirectory;

        public void Add(IPackageTree item)
        {
            throw new NotImplementedException();
        }

        public void Remove(IPackageTree item)
        {
            throw new NotImplementedException();
        }

        public IPackageTree Parent
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IPackageTree[] Children
        {
            get { throw new NotImplementedException(); }
        }

        public IPackageTree Root
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsRoot
        {
            get { throw new NotImplementedException(); }
        }

        public bool Exists
        {
            get { throw new NotImplementedException(); }
        }

        public string BuildFile
        {
            get { throw new NotImplementedException(); }
        }

        public void CreateRequiredDirectories()
        {
            throw new NotImplementedException();
        }

        public IPackageTree RetrievePackage(string packageName)
        {
            return this;
        }

        public IBuildMetaData BuildMetaData
        {
            get { throw new NotImplementedException(); }
        }

        public IBuildMetaData GetBuildMetaData(string packageName)
        {
            throw new NotImplementedException();
        }

        public DirectoryInfo CurrentDirectory
        {
            get { throw new NotImplementedException(); }
        }

        public FileInfo Nant
        {
            get { throw new NotImplementedException(); }
        }

        public FileInfo Sn
        {
            get { throw new NotImplementedException(); }
        }

        public DirectoryInfo WorkingDirectory
        {
            get { return new DirectoryInfo(Path.Combine(baseDirectory, "working")); }
        }

        public bool IsBuildNode
        {
            get { throw new NotImplementedException(); }
        }

        public DirectoryInfo OutputDirectory
        {
            get { return new DirectoryInfo(Path.Combine(baseDirectory, "output")); }
            set { throw new NotImplementedException(); }
        }

        public List<IPackageTree> BuildNodes()
        {
            throw new NotImplementedException();
        }

        public IRevisionData GetRevisionData()
        {
            throw new NotImplementedException();
        }

        public FakePackageTree(string baseDirectory)
        {
            this.baseDirectory = baseDirectory;
        }
    }
}