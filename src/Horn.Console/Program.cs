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
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            log.Debug("Horn starting.........");

            XmlConfigurator.Configure();

            var output = new StringWriter();

            var parser = new SwitchParser(output, args);

            if(!parser.IsAValidRequest())
            {
                log.Error(output.ToString());
                return;
            }

            InitialiseIoC();

            //TODO: Move to PackageTree
            var packageTree = GetPackageTree();

            IoC.Resolve<IPackageCommand>(parser.ParsedArgs.First().Key).Execute(packageTree, parser.ParsedArgs);
        }

        private static void InitialiseIoC()
        {
            var resolver = new WindsorDependencyResolver();

            IoC.InitializeWith(resolver);

            log.Debug("IOC initialised.....");
        }

        //TODO: Move to PackageTree
        private static IPackageTree GetPackageTree()
        {
            IPackageTree root = new PackageTree(GetRootFolderPath(), null);

            //HACK: Remember to remove
            //return root;

            //TODO: Hard coded dependency.  Should be injected in or retrieved from the container
            IMetaDataSynchroniser metaDataSynchroniser =
                new MetaDataSynchroniser(new SVNSourceControl(MetaDataSynchroniser.PACKAGE_TREE_URI));

            metaDataSynchroniser.SynchronisePackageTree(root);

            return root;
        }

        private static DirectoryInfo GetRootFolderPath()
        {
            var documents = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            var rootFolder = Path.Combine(documents.Parent.FullName, PackageTree.RootPackageTreeName);

            log.DebugFormat("root folder = {0}", rootFolder);

            var ret = new DirectoryInfo(rootFolder);

            ret.Create();

            return ret;
        }
    }
}