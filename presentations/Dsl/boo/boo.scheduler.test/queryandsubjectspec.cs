using System;
using boo.scheduler.core.domain;
using boo.scheduler.core.dsl;
using boo.scheduler.core.dsl.compilersteps;
using NUnit.Framework;
using Rhino.DSL;

namespace boo.scheduler.test
{
    public class When_the_task_requires_a_query : DslSpecBase
    {
        protected override string BooFileUndertest
        {
            get { return "selectblock.boo"; }
        }

        [Test]
        public void Then_the_query_is_retrieved_from_the_dsl()
        {
            Assert.That(_task.Query, Is.EqualTo("SELECT fullname, email FROM Users WHERE DATEDIFF(d, lastlogindate, getdate()) > 82"));

            Assert.That(_task.Subject, Is.EqualTo("ACME Password Expiry"));
        }
    }
}