using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Horn.Core.PackageStructure;
using Horn.Core.Utils;
using Horn.Core.Utils.Framework;
using log4net;


namespace Horn.Core.BuildEngines
{
    public class BuildEngine
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MSBuildBuildTool));

        public string BuildFile { get; private set; }

        public Dictionary<string, string> MetaData { get; set; }

        public FrameworkVersion Version { get; private set; }

        public List<string> Tasks { get; private set; }

        public IBuildTool BuildTool { get; private set; }

        public Dictionary<string, string> Parameters{ get; private set;}

        public List<Dependency> Dependencies { get; private set; }

        public string OutputDirectory { get; set; }

        public void AssignParameters(string[] parameters)
        {
            if ((parameters == null) || (parameters.Length == 0))
                return;

            Parameters = new Dictionary<string, string>();

            parameters.ForEach(x =>
                                   {
                                       var parts = x.Split('=');
                                        
                                       Parameters.Add(parts[0], parts[1]);
                                   });
        }

        public virtual void AssignTasks(string[] tasks)
        {
            Tasks = new List<string>(tasks);
        }

        private void CopyDependenciesTo(IPackageTree packageTree)
        {
            foreach (Dependency dependency in Dependencies)
            {
                var outputPath = packageTree.RetrievePackage(dependency.PackageName).OutputDirectory.FullName;

                var buildDirectory = new DirectoryInfo(outputPath);

                var sourceFiles = buildDirectory.GetFiles();

                var possibleTargetDir = packageTree.WorkingDirectory.GetDirectories(dependency.Library);

                string targetDir;

                if (possibleTargetDir.Length == 0)
                {
                    targetDir = packageTree.WorkingDirectory.CreateSubdirectory(dependency.Library).FullName;
                }
                else
                {
                    targetDir = possibleTargetDir[0].FullName;
                }


                foreach (FileInfo nextFile in sourceFiles)
                {
                    log.InfoFormat("Dependency: Copying {0} to {1} ...", nextFile.FullName, targetDir);
                    nextFile.CopyTo(Path.Combine(targetDir, nextFile.Name), true);
                }
            }
        }

        public virtual BuildEngine Build(IProcessFactory processFactory, IPackageTree packageTree)
        {
            string pathToBuildFile = GetBuildFilePath(packageTree);

            var cmdLineArguments = BuildTool.CommandLineArguments(pathToBuildFile, this, packageTree, Version);

            var pathToBuildTool = BuildTool.PathToBuildTool(packageTree, Version);

            CopyDependenciesTo(packageTree);

            IProcess process = processFactory.GetProcess(pathToBuildTool, cmdLineArguments, packageTree.WorkingDirectory.FullName);

            while (true)
            {
                string line = process.GetLineOrOutput();

                if (line == null)
                    break;

                log.Info(line);
            }

            process.WaitForExit();

            DirectoryInfo buildDir = GetDirFromParts(packageTree.WorkingDirectory, OutputDirectory);

            if(packageTree.OutputDirectory.Exists)
                packageTree.OutputDirectory.Delete(true);

            packageTree.OutputDirectory.Create();

            foreach (var directory in buildDir.GetDirectories())
            {
                var artifact = Path.Combine(packageTree.OutputDirectory.FullName,
                                                   directory.Name);

                if(Directory.Exists(artifact))
                    Directory.Delete(artifact, true);

                Directory.Move(directory.FullName, artifact);
            }

            foreach (var file in buildDir.GetFiles())
            {
                var outputFile = Path.Combine(packageTree.OutputDirectory.FullName, Path.GetFileName(file.FullName));

                if(File.Exists(outputFile))
                    File.Delete(outputFile);

                File.Copy(file.FullName, outputFile);
            }

            return this;
        }

        private DirectoryInfo GetDirFromParts(DirectoryInfo sourceDirectory, string parts)
        {
            var outputPath = sourceDirectory.FullName;

            foreach (var part in parts.Split('/'))
            {
                outputPath = Path.Combine(outputPath, part);
            }

            return new DirectoryInfo(outputPath);
        }

        private string GetBuildFilePath(IPackageTree tree)
        {
            return Path.Combine(tree.WorkingDirectory.FullName, BuildFile).Replace('/', '\\');
        }

        public BuildEngine(IBuildTool buildTool, string buildFile, FrameworkVersion version)
        {
            BuildTool = buildTool;

            BuildFile = buildFile;

            Version = version;

            MetaData = new Dictionary<string, string>();

            Dependencies = new List<Dependency>();
        }

        public void AssignMataData(string[] parameters)
        {
            if ((parameters == null) || (parameters.Length == 0))
                return;

            parameters.ForEach(x =>
            {
                var parts = x.Split('=');

                MetaData.Add(parts[0], parts[1]);
            });
        }
    }
}
