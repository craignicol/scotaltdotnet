using System;
using boo.scheduler.core.domain;
using boo.scheduler.core.dsl;
using boo.scheduler.core.dsl.compilersteps;
using Rhino.DSL;

namespace boo.scheduler.test
{
    public abstract class DslSpecBase : ContextSpecification
    {
        protected DslFactory _factory;
        protected Task _task;
        protected BaseScheduler _baseScheduler;

        protected abstract string BooFileUndertest { get; }

        protected override void establish_context()
        {
            _factory = new DslFactory
                           {
                               BaseDirectory = AppDomain.CurrentDomain.BaseDirectory
                           };

            _factory.Register<BaseScheduler>(new SchedulerDslEngine());
        }

        protected override void because()
        {
            _baseScheduler = _factory.Create<BaseScheduler>(BooFileUndertest);

            _baseScheduler.Prepare();

            _task = _baseScheduler.Task;
        }
    }
}