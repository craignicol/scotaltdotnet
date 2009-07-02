using core.domain;
using MbUnit.Framework;

namespace core.tests.Mapping
{
    public class When_we_need_to_store_user_details : NhibernateSpecBase
    {
        private User _user;

        protected override void establish_context()
        {
            _user = new User
                        {
                            FirstName = "Paul",
                            Surname = "Cowan",
                            Email = "dagda1@scotalt.net"
                        };
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
        private User _user;

        protected override void establish_context()
        {
        }

        protected override void because()
        {
        }

        [Test]
        public void Then_you_can_use_the_criteria_syntax()
        {
            Assert.AreEqual(_user.Email, "dagda1@scotalt.net");
        }

        [Test]
        public void Or_you_can_use_hql()
        {
            Assert.AreEqual(_user.Email, "dagda1@scotalt.net");
        }
    }

    public class When_a_user_is_no_longer_in_ther_organisation : NhibernateSpecBase
    {
        private User _user;

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
        private Role _role;

        protected override void establish_context()
        {
            _role = new Role {Name = "System Admin"};
        }

        protected override void because()
        {
        }

        [Test]
        public void Then_a_role_should_be_maintained()
        {
            Assert.Greater(_role.Id, 0);
        }
    }

    public class When_we_need_different_user_permissions : NhibernateSpecBase
    {
        private Permission _permission;

        protected override void establish_context()
        {
            _permission = new Permission {Name = "Customers"};
        }

        protected override void because()
        {
        }

        [Test]
        public void Then_a_permission_should_be_maintained()
        {
            Assert.Greater(_permission.Id, 0);
        }
    }
}