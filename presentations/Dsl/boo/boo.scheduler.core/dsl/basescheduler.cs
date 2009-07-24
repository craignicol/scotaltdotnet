using System;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;
using boo.scheduler.core.domain;

namespace boo.scheduler.core.dsl
{
    public abstract class BaseScheduler
    {
        public Task Task { get; private set; }

        [Meta]
        public static Expression task(StringLiteralExpression taskName, BlockExpression action)
        {
            return new MethodInvocationExpression(
                                    new ReferenceExpression("CreateTask"),
                                    taskName,
                                    action
                                );
        }

        public abstract void Prepare();

        public virtual void CreateTask(string taskName, Action taskDetail)
        {
            Task = new Task(taskName, TimeSpan.Zero);

            taskDetail();
        }
    }
}