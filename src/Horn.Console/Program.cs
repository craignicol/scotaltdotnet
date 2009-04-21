using System;
using System.IO;
using Horn.Core.PackageCommands;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Core.Utils.CmdLine;
using Horn.Core.Utils.IoC;
using log4net;
using log4net.Config;

namespace Horn.Console
{
    using Boo.Lang;
    using Console=System.Console;

    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            log.Debug("Horn starting.........");
            
            CommandLineArguments commandLineArgs = GetCommandLineArgs(args);

            if (!IsAValidRequest(commandLineArgs))
            {
                commandLineArgs.PrintOptions();
                return;
            }
            
            XmlConfigurator.Configure();

            InitialiseIoC();

            var info = GetRootFolderPath(commandLineArgs);
            var packageTree = GetPackageTree(info);

            IoC.Resolve<IPackageCommand>("install").Execute(packageTree, commandLineArgs);
        }

        private static bool IsAValidRequest(CommandLineArguments arguments)
        {
            return arguments.PackageName != null;
        }

        private static CommandLineArguments GetCommandLineArgs(string[] args)
        {
            var commandLineArgs = new CommandLineArguments();
            commandLineArgs.Parse(args);
            LogArguments(commandLineArgs);
            return commandLineArgs;
        }

        private static void InitialiseIoC()
        {
            var resolver = new WindsorDependencyResolver();

            IoC.InitializeWith(resolver);

            log.Debug("IOC initialised.....");
        }

        private static IPackageTree GetPackageTree(DirectoryInfo rootFolderPath)
        {
            IPackageTree root = new PackageTree(rootFolderPath, null);

            //TODO: Hard coded dependency.  Should be injected in or retrieved from the container
            IMetaDataSynchroniser metaDataSynchroniser =
                new MetaDataSynchroniser(new SVNSourceControl(MetaDataSynchroniser.PACKAGE_TREE_URI));

            metaDataSynchroniser.SynchronisePackageTree(root);

            return new PackageTree(rootFolderPath, null);
        }

        private static DirectoryInfo GetRootFolderPath(CommandLineArguments arguments)
        {
            string rootFolder = LoadRootPath(arguments);

            log.DebugFormat("root folder = {0}", rootFolder);

            var ret = new DirectoryInfo(rootFolder);

            ret.Create();

            return ret;
        }

        // HACK: Quick and dirty fix for allowing for a different path to be define and rememebered
        private static string LoadRootPath(CommandLineArguments arguments)
        {
            string rootFolder;
            if (!string.IsNullOrEmpty(arguments.Path))
            {
                File.WriteAllText("horn.ini", arguments.Path);
                return arguments.Path;
            }

            if (File.Exists("horn.ini"))
            {
                return File.ReadAllText("horn.ini");
            }

            rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            return rootFolder;
        }

        private static void LogArguments(CommandLineArguments args)
        {
            log.Info(args);
        }



    }
}