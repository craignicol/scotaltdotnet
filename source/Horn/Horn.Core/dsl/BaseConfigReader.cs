using System;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;
using Horn.Core.SCM;

namespace Horn.Core.dsl
{
    public abstract class BaseConfigReader
    {
        public abstract void Prepare();

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

        public void GetInstallerMeta(string installName, Action installDelegate)
        {
            InstallName = installName;

            installDelegate();
        }

        protected void SetBuildTargets(string[] taskActions)
        {
            BuildEngine.AssignTasks(taskActions);
        }

        [Meta]
        public static Expression GetFrom(MethodInvocationExpression get)
        {
            return get;
        }

        protected void svn(string url)
        {
            SourceControl = SourceControl.Create<SVNSourceControl>(url);
        }

        [Meta]
        public static Expression BuildWith(ReferenceExpression builder, MethodInvocationExpression build)
        {
            var targetName = ((ReferenceExpression)builder).Name;

            return new MethodInvocationExpression(
                    new ReferenceExpression(targetName),
                    build.Arguments[0]
                );
        }

        protected void nant(string buildFile, Action action)
        {
            BuildEngine = new BuildEngine(new NAntBuildTool(), buildFile);

            action();
        }

        protected void msbuild(string buildFile)
        {
            BuildEngine = new BuildEngine(new MSBuildBuildTool(), buildFile);
        }


        protected void rake(string buildFile, Action action)
        {
            BuildEngine = new BuildEngine(new RakeBuildTool(), buildFile);

            action();
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

        public void description(string text)
        {
            Description = text;
        }

        #region for testing only

        public virtual string InstallName { get; set; }

        public virtual string Description { get; set; }

        public virtual SourceControl SourceControl { get; set; }

        public virtual BuildEngine BuildEngine { get; set; }

        #endregion
    }
}