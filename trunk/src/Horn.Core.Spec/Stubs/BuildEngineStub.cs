using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.Dependencies;
using Horn.Core.Utils.Framework;

namespace Horn.Core.Spec.Doubles
{
    public class BuildEngineStub : BuildEngines.BuildEngine
    {

        public override Horn.Core.BuildEngines.BuildEngine Build(IProcessFactory processFactory, Horn.Core.PackageStructure.IPackageTree packageTree)
        {
            return this;
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