using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var rootFolder = GetRootFolderPath();

            //TODO: Hard coded dependency.  Should be injected in or retrieved from the container
            IMetaDataSynchroniser metaDataSynchroniser =
                new MetaDataSynchroniser(new SVNSourceControl(MetaDataSynchroniser.PACKAGE_TREE_URI));

            if(!metaDataSynchroniser.PackageTreeExists(rootFolder.FullName))
                metaDataSynchroniser.SynchronisePackageTree(rootFolder.FullName);

            return new PackageTree(rootFolder, null);
        }

        //TODO: to be replaced by user defined choice from the install perhaps?
        private static DirectoryInfo GetRootFolderPath()
        {
            var documents = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            var rootFolder = string.Format("{0}\\Horn", documents.Parent.FullName);

            log.DebugFormat("root folder = {0}", rootFolder);

            //TODO: remove delete of package tree on every run when a bit more stable.
            if (Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);

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