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

        public void install(string installName, ActionDelegate installDelegate)
        {
            InstallName = installName;

            installDelegate();
        }

        protected void SetBuildTargets(Action  action)
        {
            Console.WriteLine(action.GetType()); 
        }


        protected void Tasks(ActionDelegate action)
        {
            Console.WriteLine(action);

        }

        [Meta]
        public static Expression tasks(Expression expression)
        {
            BlockExpression condition = new BlockExpression();
            condition.Body.Add(new ReturnStatement(expression));
            return new MethodInvocationExpression(
                new ReferenceExpression("Tasks"),
                condition
            );
        }

        //[Meta]
        //public static Expression tasks(params ReferenceExpression[] expressions)
        //{
        //    var buildTasks = new List<string>();

        //    foreach (var expression in expressions)
        //        buildTasks.Add(expression.Name);

        //    var block = new Block();

        //    return null;
        //}


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

        #endregion

        protected BaseConfigReader()
        {
            BuildTasks = new List<string>();
        }

    }

    public class one
    {
        public void DoIt(){Console.WriteLine("Test");}
    }
}