using System;
using Horn.Core.BuildEngines;
using Horn.Core.Dsl;
using Horn.Core.GetOperations;
using Horn.Core.PackageCommands;
using Horn.Core.PackageStructure;

namespace Horn.Core.Spec.Doubles
{
    public class PackageBuilderStub : PackageBuilder
    {
        protected override void ExecuteBuild(IPackageTree nextTree, IBuildMetaData nextMetaData)
        {
            Console.WriteLine(string.Format("Building {0}", nextMetaData.InstallName));
        }

        public PackageBuilderStub(IGet get, IProcessFactory processFactory) : base(get, processFactory)
        {
        }
    }
}