using System;
using System.Collections.Generic;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;
using Horn.Core.BuildEngines;
using Horn.Core.SCM;
using Horn.Core.Utils.Framework;

namespace Horn.Core.Dsl
{
    using Dependencies;

    public abstract class BooConfigReader
    {
        public virtual BuildEngine BuildEngine { get; set; }

        public virtual string Description { get; set; }

        public List<ExportData> ExportList { get; set; }

        public virtual string InstallName { get; set; }

        public virtual PackageMetaData PackageMetaData
        {
            get
            {
                return Global.package;
            }
        }

        public virtual List<string> PrebuildCommandList { get; set; }

        public virtual SourceControl SourceControl { get; set; }


        public void AddDependencies(string[] dependencies)
        {
            Array.ForEach(dependencies, item =>
                                     {
                                         var dependency = Dependency.Parse(item);

                                         BuildEngine.Dependencies.Add(dependency); 
                                     });
        }

        public void AddSwitches(Action parametersDelegate)
        {
            parametersDelegate();
        }

        public void AssignTasks(Action tasksDelegate)
        {
            tasksDelegate();
        }

        [Meta]
        public static Expression build_with(ReferenceExpression builder, MethodInvocationExpression build, ReferenceExpression frameWorkVersion)
        {
            var targetName = builder.Name;

            return new MethodInvocationExpression(
                    new ReferenceExpression(targetName),
                    build.Arguments[0],
                    new StringLiteralExpression(frameWorkVersion.Name)
                );
        }

        [Meta]
        public static Expression data(params StringLiteralExpression[] expressions)
        {
            var arrayExpression = new ArrayLiteralExpression();

            for (var i = 0; i < expressions.Length; i++)
                arrayExpression.Items.Add(expressions[i]);

            return new MethodInvocationExpression(
                new ReferenceExpression("SetData"),
                arrayExpression
            );
        }

        [Meta]
        public static Expression dependencies(MethodInvocationExpression addDependencyMethod)
        {
            return addDependencyMethod;
        }

        public void description(string text)
        {
            Description = text;
        }

        public virtual void generate_strong_key()
        {
            BuildEngine.GenerateStrongKey = true;
        }

        [Meta]
        public static Expression get_from(MethodInvocationExpression get)
        {
            return get;
        }

        public void GetInstallerMeta(string installName, Action installDelegate)
        {
            InstallName = installName;

            installDelegate();
        }

        [Meta]
        public static Expression install(ReferenceExpression expression, 
                                                        BlockExpression action)
        {
            var installName = new StringLiteralExpression(expression.Name);

            return new MethodInvocationExpression(
                    new ReferenceExpression("GetInstallerMeta"),
                    installName,
                    action
                );
        }

        public void output(string path)
        {
            BuildEngine.OutputDirectory = path;   
        }

        [Meta]
        public static Expression parameters(params StringLiteralExpression[] expressions)
        {
            var arrayExpression = new ArrayLiteralExpression();

            for (var i = 0; i < expressions.Length; i++)
                arrayExpression.Items.Add(expressions[i]);

            return new MethodInvocationExpression(
                new ReferenceExpression("SetParameters"),
                arrayExpression
            );
        }

        public void ParseCommands(string[] cmdList)
        {
            PrebuildCommandList = new List<string>(cmdList);
        }

        public void ParseExportList(string[][] exports)
        {

            for(var i =0;i < exports[0].Length; i++)
            {
                var sourceControlType = (SourceControlType) Enum.Parse(typeof (SourceControlType), exports[i][0]);

                ExportList.Add(new ExportData(exports[i][1], sourceControlType));
            }
        }

        [Meta]
        public static Expression export(BlockExpression exportUrls)
        {
            var exportList = new ArrayLiteralExpression();

            foreach (Statement statement in exportUrls.Body.Statements)
            {
                var expression = (MethodInvocationExpression)((ExpressionStatement)statement).Expression;

                var innerArray = new ArrayLiteralExpression();

                innerArray.Items.Add(new StringLiteralExpression(expression.Target.ToString()));
                innerArray.Items.Add(new StringLiteralExpression(expression.Arguments[0].ToString().Trim(new char[] { '\'' })));

                exportList.Items.Add(innerArray);
            }

            return new MethodInvocationExpression(new ReferenceExpression("ParseExportList"), exportList);
        }

        [Meta]
        public static Expression prebuild(BlockExpression commands)
        {
            var cmdList = new ArrayLiteralExpression();
            
            foreach (Statement statement in commands.Body.Statements)
            {
                var expression = (MethodInvocationExpression)((ExpressionStatement) statement).Expression;

                cmdList.Items.Add(new StringLiteralExpression(expression.Arguments[0].ToString().Trim(new char[]{'\''})));
            }

            return new MethodInvocationExpression(new ReferenceExpression("ParseCommands"), cmdList);
        }

        public abstract void Prepare();

        public void shared_library(string sharedLib)
        {
            BuildEngine.SharedLibrary = sharedLib;
        }

        [Meta]
        public static Expression switches(Expression action)
        {
            return new MethodInvocationExpression(
                    new ReferenceExpression("AddSwitches"),
                    action
                );
        }

        [Meta]
        public static Expression tasks(params ReferenceExpression[] expressions)
        {
            var arrayExpression = new ArrayLiteralExpression();

            for (var i = 0; i < expressions.Length; i++)
                arrayExpression.Items.Add(new StringLiteralExpression(expressions[i].Name));

            return new MethodInvocationExpression(
                new ReferenceExpression("SetBuildTargets"),
                arrayExpression
            );
        }

        [Meta]
        public static Expression with(Expression action)
        {
            return new MethodInvocationExpression(
                    new ReferenceExpression("AssignTasks"), 
                    action
                );
        }

        protected void msbuild(string buildFile, string frameWorkVersion)
        {
            var version = (FrameworkVersion)Enum.Parse(typeof(FrameworkVersion), frameWorkVersion);

            SetBuildEngine(new MSBuildBuildTool(), buildFile, version);
        }

        protected void nant(string buildFile, string frameWorkVersion)
        {
            var version = (FrameworkVersion)Enum.Parse(typeof(FrameworkVersion), frameWorkVersion);

            SetBuildEngine(new NAntBuildTool(), buildFile, version);
        }

        protected void rake(string buildFile, Action action, string frameWorkVersion)
        {
            var version = (FrameworkVersion)Enum.Parse(typeof(FrameworkVersion), frameWorkVersion);

            SetBuildEngine(new RakeBuildTool(), buildFile, version);

            action();
        }

        protected void SetBuildTargets(string[] taskActions)
        {
            BuildEngine.AssignTasks(taskActions);
        }

        protected void SetParameters(string[] parameters)
        {
            BuildEngine.AssignParameters(parameters);
        }

        protected void svn(string url)
        {
            SourceControl = SourceControl.Create<SVNSourceControl>(url);
        }



        private void SetBuildEngine(IBuildTool tool, string buildFile, FrameworkVersion version)
        {
            BuildEngine = new BuildEngine(tool, buildFile, version, IoC.Resolve<IDependencyDispatcher>());
        }


        protected BooConfigReader()
        {
            ExportList = new List<ExportData>();

            Global.package.PackageInfo.Clear();
        }

    }
}