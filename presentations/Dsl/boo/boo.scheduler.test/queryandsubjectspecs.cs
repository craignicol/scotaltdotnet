using NUnit.Framework;

namespace boo.scheduler.test
{
    public class When_the_task_needs_a_list_of_contacts_to_email : DslSpecBase
    {
        protected override string BooFileUnderTest
        {
            get { return "selectblock.boo"; }
        }

        [Test]
        public void Then_the_contact_list_query_is_specified_in_the_dsl()
        {
            Assert.That(_task.Query, Is.EqualTo("SELECT fullname, email FROM Users WHERE DATEDIFF(d, lastlogindate, getdate()) > 82"));
        }
    }

    public class When_the_we_need_a_subject_for_the_task_email: DslSpecBase
    {
        protected override string BooFileUnderTest
        {
            get { return "selectblock.boo"; }
        }   
     
        [Test]
        public void Then_we_can_state_the_subject_in_the_dsl()
        {
            Assert.That(_task.Subject, Is.EqualTo("ACME Password Expiry"));
        }
    }
}