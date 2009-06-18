﻿using System;
using System.IO;
using System.Linq;
using Horn.Core;
using Horn.Core.PackageCommands;
using Horn.Core.PackageStructure;
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

            var packageTree = IoC.Resolve<IPackageTree>().GetRootPackageTree(GetRootFolderPath());

            try
            {
                IoC.Resolve<IPackageCommand>(parser.ParsedArgs.First().Key).Execute(packageTree, parser.ParsedArgs);
            }
            catch (UnkownInstallPackageException unk)
            {
                log.Info(unk.Message);
            }

        }

        private static void InitialiseIoC()
        {
            var resolver = new WindsorDependencyResolver();

            IoC.InitializeWith(resolver);

            log.Debug("IOC initialised.....");
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