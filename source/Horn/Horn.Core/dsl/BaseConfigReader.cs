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
            BlockExpression blockExpression = GetBlockExpression(expression);

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

        protected void SetBuildTargets(Func<string[]> tasksAction)
        {
            foreach(var task in tasksAction())
                BuildTasks.Add(task);
        }

        [Meta]
        public static Expression tasks(params ReferenceExpression[] expressions)
        {
            var blockExpression = new BlockExpression();

            var arrayExpression = new ArrayLiteralExpression();

            for (var i = 0; i < expressions.Length; i++)
                arrayExpression.Items.Add(new StringLiteralExpression(expressions[i].Name));

            blockExpression.Body.Add(new ReturnStatement(arrayExpression));

            return new MethodInvocationExpression(
                new ReferenceExpression("SetBuildTargets"),
                blockExpression
            );
        }

        private static BlockExpression GetBlockExpression(ReferenceExpression expression)
        {
            var blockExpression = new BlockExpression();

            var returnExpression = new StringLiteralExpression(expression.Name);

            blockExpression.Body.Add(new ReturnStatement(returnExpression));
            return blockExpression;
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