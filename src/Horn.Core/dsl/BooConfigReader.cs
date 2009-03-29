using System;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;
using Horn.Core.BuildEngines;
using Horn.Core.SCM;
using Horn.Core.Utils.Framework;
using m = Microsoft.Scripting;

namespace Horn.Core.Dsl
{
    public abstract class BooConfigReader
    {
        public delegate void Action();

        public virtual BuildEngine BuildEngine { get; set; }

        public virtual string Description { get; set; }

        public virtual string InstallName { get; set; }

        public virtual SourceControl SourceControl { get; set; }

        public abstract void Prepare();

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
        public static Expression dependencies(MethodInvocationExpression addDependencyMethod)
        {
            return addDependencyMethod;
        }

        public virtual void generate_strong_key()
        {
            BuildEngine.GenerateStrongKey = true;
        }

        public void AddDependencies(string[] dependencies)
        {
            Array.ForEach(dependencies, item =>
                                     {
                                         var dependency = new Dependency(item.Split('|')[0], item.Split('|')[1]);

                                         BuildEngine.Dependencies.Add(dependency); 
                                     });
        }

        [Meta]
        public static Expression get_from(MethodInvocationExpression get)
        {
            return get;
        }

        [Meta]
        public static Expression install(ReferenceExpression expression, Expression action)
        {
            var installName = new StringLiteralExpression(expression.Name);

            return new MethodInvocationExpression(
                    new ReferenceExpression("GetInstallerMeta"),
                    installName,
                    action
                );
        }


        [Meta]
        public static Expression metadata(Expression action)
        {
            return new MethodInvocationExpression(
                    new ReferenceExpression("AddMetaData"),
                    action
                );
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

        public void AddSwitches(Action parametersDelegate)
        {
            parametersDelegate();
        }

        public void AddMetaData(Action dataDelegate)
        {
            dataDelegate();
        }

        public void AssignTasks(Action tasksDelegate)
        {
            tasksDelegate();
        }

        public void description(string text)
        {
            Description = text;
        }

        public void shared_library(string sharedLib)
        {
            BuildEngine.SharedLibrary = sharedLib;
        }

        public void output(string path)
        {
            BuildEngine.OutputDirectory = path;   
        }

        public void GetInstallerMeta(string installName, Action installDelegate)
        {
            InstallName = installName;

            installDelegate();
        }

        protected void SetParameters(string[] parameters)
        {
            BuildEngine.AssignParameters(parameters);
        }

        protected void SetData(string[] parameters)
        {
            BuildEngine.AssignMataData(parameters);
        }

        protected void SetBuildTargets(string[] taskActions)
        {
            BuildEngine.AssignTasks(taskActions);
        }

        protected void svn(string url)
        {
            SourceControl = SourceControl.Create<SVNSourceControl>(url);
        }

        protected void nant(string buildFile, string frameWorkVersion)
        {
            var version = (FrameworkVersion)Enum.Parse(typeof(FrameworkVersion), frameWorkVersion);

            BuildEngine = new BuildEngine(new NAntBuildTool(), buildFile, version);
        }

        protected void msbuild(string buildFile, string frameWorkVersion)
        {
            var version = (FrameworkVersion)Enum.Parse(typeof(FrameworkVersion), frameWorkVersion);

            BuildEngine = new BuildEngine(new MSBuildBuildTool(), buildFile, version);
        }

        protected void rake(string buildFile, Action action, string frameWorkVersion)
        {
            var version = (FrameworkVersion)Enum.Parse(typeof(FrameworkVersion), frameWorkVersion);

            BuildEngine = new BuildEngine(new RakeBuildTool(), buildFile, version);

            action();
        }
    }
}