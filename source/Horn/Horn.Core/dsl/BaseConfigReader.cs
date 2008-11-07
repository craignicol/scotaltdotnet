using System;
using System.Collections.Generic;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;

namespace Horn.Core.dsl
{
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
            foreach (var task in taskActions)
                BuildTasks.Add(task);
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

        public void buildfile(string file)
        {
            BuildFile = file;
        }

        public void description(string text)
        {
            Description = text;
        }

        public void svn(string url)
        {
            Svn = url;
        }

        #region for testing only

        public ActionDelegate Action { get; set; }

        public string InstallName{ get; set; }

        public string Description { get; set; }

        public string Svn { get; set; }

        public List<string> BuildTasks{ get; set;}

        public string BuildFile { get; set; }

        #endregion

        protected BaseConfigReader()
        {
            BuildTasks = new List<string>();
        }

    }
}