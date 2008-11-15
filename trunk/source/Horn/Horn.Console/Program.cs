using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Horn.Core.PackageCommands;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.CmdLine;
using Horn.Core.Utils.IoC;

namespace Horn.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialiseIoC();

            var output = new StringWriter();

            var packageTree = GetPackageTree();

            var parser = new SwitchParser(output, packageTree);  

            var parsedArgs = parser.Parse(args);

            if (!IsAValidRequest(parser, parsedArgs))
            {
                System.Console.WriteLine(output.ToString());
                return;
            }

            IoC.Resolve<IPackageCommand>(parsedArgs.First().Key).Execute(packageTree parsedArgs);
        }

        private static void InitialiseIoC()
        {
            var resolver = new WindsorDependencyResolver();

            IoC.InitializeWith(resolver);          
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
    }
}