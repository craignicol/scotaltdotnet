using NUnit.Framework;

namespace boo.scheduler.test
{
    public class When_we_want_to_record_the_task_frequency : DslSpecBase
    {
        protected override string BooFileUnderTest
        {
            get { return "frequency.boo"; }
        }

        [Test]
        public void Then_the_next_poll_time_shoud_be_starting_date_dlus_frequency()
        {
            var nextPollTime = _task.StartingTime.AddDays(_task.Frequency.Days);

            Assert.That(_task.NextPollTime.ToShortDateString(),
                        Is.EqualTo(nextPollTime.ToShortDateString()));
        }
    }
}