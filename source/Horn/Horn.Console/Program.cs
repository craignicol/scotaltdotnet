using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Horn.Core.PackageCommands;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.CmdLine;
using Horn.Core.Utils.IoC;
using log4net;
using log4net.Config;
using log4net.Util;

namespace Horn.Console
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (Program));

        static void Main(string[] args)
        {
            log.Debug("Horn starting.........");

            XmlConfigurator.Configure();

            InitialiseIoC();

            var output = new StringWriter();

            var packageTree = GetPackageTree();

            var parser = new SwitchParser(output, packageTree);  

            var parsedArgs = parser.Parse(args);

            if (!IsAValidRequest(parser, parsedArgs))
            {
                log.Error(output.ToString());
                return;
            }

            LogArguments(parsedArgs);

            IoC.Resolve<IPackageCommand>(parsedArgs.First().Key).Execute(packageTree, parsedArgs);
        }

        private static void InitialiseIoC()
        {
            var resolver = new WindsorDependencyResolver();

            IoC.InitializeWith(resolver);          

            log.Debug("IOC initialised.....");
        }

        private static PackageTree GetPackageTree()
        {
            //HACK: Replace as soon as
            var hornBuildFile = string.Format("{0}build.boo", AppDomain.CurrentDomain.BaseDirectory);

            var rootFolder = GetRootFolderPath();

            PackageTree.CreateDefaultTreeStructure(rootFolder.FullName, hornBuildFile);

            return new PackageTree(rootFolder, null);
        }

        //HACK: to be replaced by user defined choice from the install perhaps?
        private static DirectoryInfo GetRootFolderPath()
        {
            var documents = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            var rootFolder = string.Format("{0}\\Horn", documents.Parent.FullName);

            log.DebugFormat("root folder = {0}", rootFolder);

            if(Directory.Exists(rootFolder))
                return new DirectoryInfo(rootFolder);

            var ret = new DirectoryInfo(rootFolder);

            ret.Create();

            return ret;
        }

        private static bool IsAValidRequest(SwitchParser parser, IDictionary<string, IList<string>> parsedArgs)
        {
            if (IsHelpTextSwitch(parsedArgs))
                return false;

            return parser.IsValid(parsedArgs);
        }

        private static bool IsHelpTextSwitch(IDictionary<string, IList<string>> parsedArgs)
        {
            return parsedArgs != null && parsedArgs is HelpReturnValue;
        }

        private static void LogArguments(Dictionary<string, IList<string>> args)
        {
            foreach (var arg in args)
            {
                log.InfoFormat("Command {0} was issued with values:", arg.Key);

                foreach (var value in arg.Value)
                    log.InfoFormat("{0}\n", value);
            }
        }
    }
}