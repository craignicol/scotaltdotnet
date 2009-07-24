using System;
using NUnit.Framework;

namespace boo.scheduler.test
{
    public class When_the_email_task_requires_parameters : DslSpecBase
    {
        protected override string BooFileUndertest
        {
            get { return "parametersblock.boo"; }
        }

        [Test]
        public void Then_the_task_will_know_the_parameters_to_replace()
        {
            Assert.That(_task.Parameters.Count, Is.EqualTo(2));

            Assert.That(_task.Parameters[0].Name, Is.EqualTo("DisplayName"));

            Assert.That(_task.Parameters[1].Name, Is.EqualTo("Url"));
        }
    }
}