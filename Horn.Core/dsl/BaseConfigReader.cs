using System;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;
using Horn.Core.BuildEngines;
using Horn.Core.SCM;
using Horn.Core.Utils.Framework;

namespace Horn.Core.dsl
{
    public abstract class BaseConfigReader
    {

		#region public properties (4) 

        public virtual BuildEngine BuildEngine { get; set; }

        public virtual string Description { get; set; }

        public virtual string InstallName { get; set; }

        public virtual SourceControl SourceControl { get; set; }

		#endregion public properties 

		#region abstract methods (1) 

        public abstract void Prepare();

		#endregion abstract methods 

		#region public static methods (5) 

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
        public static Expression with(Expression action)
        {
            return new MethodInvocationExpression(
                    new ReferenceExpression("AssignTasks"), 
                    action
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
        public static Expression GetFrom(MethodInvocationExpression get)
        {
            return get;
        }

        [Meta]
        public static Expression BuildWith(ReferenceExpression builder, MethodInvocationExpression build, ReferenceExpression frameWorkVersion)
        {
            var targetName = builder.Name;

            return new MethodInvocationExpression(
                    new ReferenceExpression(targetName),
                    build.Arguments[0],
                    new StringLiteralExpression(frameWorkVersion.Name)
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

		#endregion public static methods 

		#region public methods (2) 

        public void AddSwitches(Action parametersDelegate)
        {
            parametersDelegate();
        }


        public void AssignTasks(Action tasksDelegate)
        {
            tasksDelegate();
        }

        public void description(string text)
        {
            Description = text;
        }

        public void GetInstallerMeta(string installName, Action installDelegate)
        {
            InstallName = installName;

            installDelegate();
        }

		#endregion public methods 

		#region protected methods (5) 

        protected void SetParameters(string[] parameters)
        {
            BuildEngine.AssignParameters(parameters);
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

		#endregion protected methods 

    }
}