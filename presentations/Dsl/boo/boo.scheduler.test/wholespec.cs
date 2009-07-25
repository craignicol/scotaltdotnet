using System;
using NUnit.Framework;

namespace boo.scheduler.test
{
    public class When_we_have_a_complete_task_definition : DslSpecBase
    {
        protected override string BooFileUnderTest
        {
            get { return "whole.boo"; }
        }

        [Test]
        public void Then_the_task_knows_which_clients()
        {
            Assert.That(_task.Clients.Count, Is.EqualTo(1));

            Assert.That(_task.Clients[0], Is.EqualTo("fakeclient"));
        }

        [Test]
        public void Then_the_next_poll_time_shoud_be_starting_date_dlus_frequency()
        {
            var nextPollTime = _task.StartingTime.AddDays(_task.Frequency.Days);

            Assert.That(_task.NextPollTime.ToShortDateString(),
                        Is.EqualTo(nextPollTime.ToShortDateString()));
        }

        [Test]
        public void Then_the_task_will_know_the_parameters_to_replace()
        {
            Assert.That(_task.Parameters.Count, Is.EqualTo(2));

            Assert.That(_task.Parameters[0].Name, Is.EqualTo("DisplayName"));

            Assert.That(_task.Parameters[1].Name, Is.EqualTo("Url"));
        }

        [Test]
        public void Then_the_query_is_retrieved_from_the_dsl()
        {
            Assert.That(_task.Query, Is.EqualTo("SELECT fullname, email FROM Users WHERE DATEDIFF(d, lastlogindate, getdate()) > 82"));

            Assert.That(_task.Subject, Is.EqualTo("ACME Password Expiry"));
        }
    }
}