using System;
using NUnit.Framework;

namespace boo.scheduler.test
{
    public class When_the_task_states_which_clients_to_include : DslSpecBase
    {
        protected override string BooFileUndertest
        {
            get { return "clientsblock.boo"; }
        }

        [Test]
        public void Then_the_task_knows_which_clients()
        {
            Assert.That(_task.Clients.Count, Is.EqualTo(1));

            Assert.That(_task.Clients[0], Is.EqualTo("fakeclient"));
        }
    }
}