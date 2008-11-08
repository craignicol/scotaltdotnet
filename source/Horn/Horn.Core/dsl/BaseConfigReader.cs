using System;
using System.Collections.Generic;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;

namespace Horn.Core.dsl
{
    using Utils;

    public abstract class BaseConfigReader
    {
        public delegate void ActionDelegate();
        public delegate bool ConditionDelegate();

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
            taskActions.ForEach(BuildTasks.Add);
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
        public static Expression build_with(MethodInvocationExpression build)
        {
            return build;
        }

        protected void nant(string buildFile)
        {
            Builder = new NAntBuild(buildFile);
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

        public ActionDelegate Action { get; set; }

        public string InstallName{ get; set; }

        public string Description { get; set; }

        public List<string> BuildTasks{ get; set;}

        public SourceControl SourceControl { get; set; }

        public Build Builder { get; set; }

        #endregion

        protected BaseConfigReader()
        {
            BuildTasks = new List<string>();
        }

    }
}