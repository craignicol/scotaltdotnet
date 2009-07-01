using MbUnit.Framework;

namespace core.tests.Mapping
{
    public class When_we_need_to_store_user_details : NhibernateSpecBase
    {
        protected override void establish_context()
        {

        }

        protected override void because()
        {
        }

        [Test]
        public void Then_the_details_should_be_maintained_in_the_database()
        {
        }
    }

    public class When_retrieving_a_users_details : NhibernateSpecBase
    {
        protected override void establish_context()
        {
        }

        protected override void because()
        {
        }

        [Test]
        public void Then_you_can_use_the_criteria_syntax()
        {
        }

        [Test]
        public void Or_you_can_use_hql()
        {
        }
    }

    public class When_a_user_is_no_longer_in_ther_organisation : NhibernateSpecBase
    {
        protected override void establish_context()
        {
        }

        protected override void because()
        {
        }

        [Test]
        public void Then_their_details_should_be_erased()
        {
        }
    }

    public class When_we_need_different_user_roles : NhibernateSpecBase
    {
        protected override void establish_context()
        {
        }

        protected override void because()
        {
        }

        [Test]
        public void Then_roles_should_be_maintained()
        {
        }
    }

    public class When_we_need_different_user_permission : NhibernateSpecBase
    {
        protected override void establish_context()
        {
        }

        protected override void because()
        {
        }

        [Test]
        public void Then_permission_should_be_maintained()
        {
        }
    }
}