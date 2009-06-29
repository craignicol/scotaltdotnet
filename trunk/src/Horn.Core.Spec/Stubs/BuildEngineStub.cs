using System;
using System.Collections.Generic;
using System.IO;
using Horn.Core.BuildEngines;
using Horn.Core.Dependencies;
using Horn.Core.Utils.Framework;

namespace Horn.Core.Spec.Doubles
{
    public class BuildEngineStub : BuildEngine
    {
        //public override BuildEngine Build(IProcessFactory processFactory, PackageStructure.IPackageTree packageTree)
        //{
        //    return this;
        //}

        protected override void CopyDependenciesTo(Horn.Core.PackageStructure.IPackageTree packageTree)
        {
            Console.WriteLine(packageTree.Name);
        }

        protected override void CopyFileFromWorkingToResult(FileInfo file, string outputFile)
        {
            Console.WriteLine(string.Format("source = {0}", file.FullName));

            Console.WriteLine(string.Format("destination = {0}", outputFile));
        }

        public BuildEngineStub(IBuildTool buildTool, IDependencyDispatcher dependencyDispatcher, List<Dependency> dependencies)
            : base(buildTool, "somefile.build", FrameworkVersion.FrameworkVersion35, dependencyDispatcher)
        {
            Dependencies = dependencies;
        }

        public BuildEngineStub(IBuildTool buildTool, string buildFile, FrameworkVersion version, IDependencyDispatcher dependencyDispatcher)
            : base(buildTool, buildFile, version, dependencyDispatcher)
        {
        }
    }
}