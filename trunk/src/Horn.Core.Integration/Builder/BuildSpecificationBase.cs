using System;
using System.IO;
using Horn.Core.BuildEngines;
using Horn.Core.extensions;
using Horn.Core.PackageStructure;
using Horn.Spec.Framework.Extensions;
using Rhino.Mocks;

namespace Horn.Core.Integration.Builder
{
    public abstract class BuildSpecificationBase : Specification
    {
        protected string workingPath;
        protected string outputPath;
        protected BuildEngine buildEngine;
        protected IPackageTree packageTree;

        protected string GetRootPath()
        {
            outputPath = CreateDirectory("Output");

            workingPath = CreateDirectory("Working");

            packageTree = MockRepository.GenerateStub<IPackageTree>();

            packageTree.Stub(x => x.OutputDirectory).Return(new DirectoryInfo(outputPath));

            var executionBase = AppDomain.CurrentDomain.BaseDirectory;

            return ResolveRootPath(executionBase);
        }

        public static string ResolveRootPath(string executionBase)
        {
            if (!IsRunningFromCIBuild)
                return new DirectoryInfo(executionBase.ResolvePath()).Parent.FullName;

            if(executionBase.IndexOf("debug") > -1)
                return new DirectoryInfo(executionBase).Parent.Parent.Parent.FullName;

            return new DirectoryInfo(executionBase).Parent.Parent.FullName;
        }

        public static bool IsRunningFromCIBuild
        {
            get
            {
                return (AppDomain.CurrentDomain.BaseDirectory.IndexOf("net-3.5") > -1);   
            }
        }

        private string CreateDirectory(string directoryName)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryName);

            if (Directory.Exists(path))
                Directory.Delete(path, true);

            Directory.CreateDirectory(path);

            return path;
        }
    }
}