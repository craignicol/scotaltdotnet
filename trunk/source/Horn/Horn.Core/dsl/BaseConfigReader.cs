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
            var blockExpression = new BlockExpression();

            var installExpression = new StringLiteralExpression(expression.Name);

            blockExpression.Body.Add(new ReturnStatement(installExpression));

            return new MethodInvocationExpression(
                    new ReferenceExpression("GetInstallerMeta"),
                    blockExpression,
                    action
                );
        }

        public void GetInstallerMeta(Func<string> installName, ActionDelegate installDelegate)
        {
            InstallName = installName();

            installDelegate();
        }

        protected void SetBuildTargets(Func<string> action)
        {
            Console.WriteLine(action()); 
        }

        [Meta]
        public static Expression tasks(ReferenceExpression expression)
        {
            var blockExpression = new BlockExpression();

            var taskExpression = new StringLiteralExpression(expression.Name);

            blockExpression.Body.Add(new ReturnStatement(taskExpression));

            return new MethodInvocationExpression(
                new ReferenceExpression("SetBuildTargets"),
                blockExpression
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