using System.Collections.Generic;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;

namespace Horn.Core.dsl
{
    using Utils;

    public abstract class BaseConfigReader
    {
        public delegate void ActionDelegate();

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

        public void GetInstallerMeta(string installName, ActionDelegate installDelegate)
        {
            InstallName = installName;

            installDelegate();
        }

        protected void SetBuildTargets(string[] taskActions)
        {
            BuildEngine.AssignTasks(taskActions);
        }

        [Meta]
        public static Expression get_from(MethodInvocationExpression get)
        {
            return get;
        }

        protected void svn(string url)
        {
            SourceControl = new SVNSourceControl(url);
        }

        [Meta]
        public static Expression build_with(MethodInvocationExpression build, ReferenceExpression with, BlockExpression action)
        {
            var targetName = ((ReferenceExpression) build.Target).Name;

            return new MethodInvocationExpression(
                    new ReferenceExpression(targetName),
                    build.Arguments[0],
                    action
                );
        }

        protected void nant(string buildFile, ActionDelegate action)
        {
            BuildEngine = new NAntBuildEngine(buildFile);

            action();
        }

        protected void rake(string buildFile, ActionDelegate action)
        {
            BuildEngine = new RakeBuildEngine(buildFile);

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

        public virtual ActionDelegate Action { get; set; }

        public virtual string InstallName { get; set; }

        public virtual string Description { get; set; }

        public virtual SourceControl SourceControl { get; set; }

        public virtual BuildEngine BuildEngine { get; set; }

        #endregion
    }
}