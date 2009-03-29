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

        private static readonly Dictionary<string, string> builtPackages = new Dictionary<string, string>();

        public string BuildFile { get; private set; }

        public Dictionary<string, string> MetaData { get; set; }

        public FrameworkVersion Version { get; private set; }

        public List<string> Tasks { get; private set; }

        public IBuildTool BuildTool { get; private set; }

        public Dictionary<string, string> Parameters{ get; private set;}

        public List<Dependency> Dependencies { get; private set; }

        public string OutputDirectory { get; set; }

        public string SharedLibrary { get; set; }

        public bool GenerateStrongKey { get; set; }

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

        public void GenerateKeyFile(IPackageTree packageTree)
        {
            string strongKey = Path.Combine(packageTree.WorkingDirectory.FullName,
                                            string.Format("{0}.snk)", packageTree.Name))
                                            ;
            string commandLine = string.Format("{0} -k {1}", packageTree.Sn, strongKey);

            ProcessStartInfo PSI = new ProcessStartInfo("cmd.exe");
            
            PSI.RedirectStandardInput = true;
            
            PSI.RedirectStandardOutput = true;
            
            PSI.RedirectStandardError = true;
            
            PSI.UseShellExecute = false;
            Process p = Process.Start(PSI);
            StreamWriter SW = p.StandardInput;
            StreamReader SR = p.StandardOutput;
            SW.WriteLine(commandLine);
            SW.Close();
        }

        public virtual BuildEngine Build(IProcessFactory processFactory, IPackageTree packageTree)
        {
            if (builtPackages.ContainsKey(packageTree.Name))
                return this;

            string pathToBuildFile = GetBuildFilePath(packageTree);

            if (GenerateStrongKey)
                GenerateKeyFile(packageTree);

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

            if (OutputDirectory == ".")
                return this;

            CopyArtifactsToBuildDirectory(packageTree);

            builtPackages.Add(packageTree.Name, packageTree.Name);

            return this;
        }

        private void CopyArtifactsToBuildDirectory(IPackageTree packageTree)
        {
            DirectoryInfo buildDir = GetDirectoryFromParts(packageTree.WorkingDirectory, OutputDirectory);

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
        }

        private void CopyDependenciesTo(IPackageTree packageTree)
        {
            foreach (Dependency dependency in Dependencies)
            {
                var dependencyDirectory = packageTree.RetrievePackage(dependency.PackageName).OutputDirectory;

                var sourceFiles = dependencyDirectory.GetFiles(string.Format("{0}.*", dependency.Library));

                var targetDir = GetDirectoryFromParts(packageTree.WorkingDirectory, SharedLibrary);

                foreach (FileInfo nextFile in sourceFiles)
                {
                    string destination = Path.Combine(targetDir.FullName, Path.GetFileName(nextFile.FullName));

                    log.InfoFormat("Dependency: Copying {0} to {1} ...", nextFile.FullName, destination);

                    nextFile.CopyTo(destination, true);
                }
            }
        }

        private DirectoryInfo GetDirectoryFromParts(DirectoryInfo sourceDirectory, string parts)
        {
            if (string.IsNullOrEmpty(parts))
                return sourceDirectory;

            var outputPath = sourceDirectory.FullName;

            if (parts.Trim() == ".")
                return sourceDirectory;

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
    }
}
