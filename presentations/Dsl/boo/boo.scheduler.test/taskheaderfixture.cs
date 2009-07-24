using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using boo.scheduler.core.domain;
using boo.scheduler.core.dsl;
using boo.scheduler.core.dsl.compilersteps;
using boo.scheduler.test;
using NUnit.Framework;
using Rhino.DSL;

namespace boo.scheduler.test
{
    public class When_the_dsl_contains_a_task : ContextSpecification
    {
        private DslFactory _factory;

        private Task _task;
        private BaseScheduler _baseScheduler;

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
            _baseScheduler = _factory.Create<BaseScheduler>("taskheader.boo");

            _baseScheduler.Prepare();

            _task = _baseScheduler.Task;
        }

        [Test]
        public void Then_the_task_description_can_be_parsed()
        {
            Assert.That(_task.Name, Is.EqualTo("90 day password reminder"));

            Assert.That(_task.EmailTemplate, Is.EqualTo("90daypasswordreminder.txt"));
        }
    }
}
