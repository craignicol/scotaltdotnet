using System;
using core.domain;
using MbUnit.Framework;
using NHibernate.Criterion;

namespace core.tests.Mapping
{
    public class When_we_need_to_store_user_details : NhibernateSpecBase
    {
        private User _user;

        protected override void establish_context()
        {
            _user = new User();

            _user.FirstName = "Paul";
            _user.Surname = "Cowan";
            _user.Email = "dagda1@scotalt.net";

            _user.Role = new Role {Id = 1, Name = "System Admin"};

            var permission = new Permission {Id = 1, Name = "Customers"};

            _user.Permissions.Add(permission.Name, permission);
        }

        protected override void because()
        {
            using(var session = _sessionFactory.OpenSession())
            using(var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(_user);

                transaction.Commit();
            }
        }

        [Test]
        public void Then_the_details_should_be_maintained_in_the_database()
        {
            Assert.AreNotEqual(_user.Uid, Guid.Empty);
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
            using (var session = _sessionFactory.OpenSession())
            {
                _user = session.CreateCriteria(typeof(User))
                    .Add(Restrictions.Eq("Email", "dagda1@scotalt.net"))
                    .UniqueResult<User>();

                Assert.AreEqual("dagda1@scotalt.net", _user.Email);

                Assert.AreEqual("System Admin", _user.Role.Name);

                Assert.AreEqual(1, _user.Permissions["Customers"].Id);
            }
        }

        [Test]
        public void Then_you_can_use_hql()
        {
            const string hql = "from User u where u.Email = :email";

            using(var session = _sessionFactory.OpenSession())
            {
                var query = session.CreateQuery(hql);

                query.SetString("email", "dagda1@scotalt.net");

                _user = query.UniqueResult<User>();
            }

            Assert.AreEqual("dagda1@scotalt.net", _user.Email);
        }
    }

    public class When_a_user_is_no_longer_in_ther_organisation : NhibernateSpecBase
    {
        private User _user;

        protected override void establish_context()
        {
            _user = GetEntityByDescription<User>("Email", "dagda1@scotalt.net");
        }

        protected override void because()
        {
            using(var session = _sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
                session.Delete(_user);

                tx.Commit();
            }

            _user = GetEntityByDescription<User>("Email", "dagda1@scotalt.net");
        }

        [Test]
        public void Then_their_details_should_be_erased()
        {
            Assert.IsNull(_user);
        }
    }

    public class When_we_need_different_user_roles : NhibernateSpecBase
    {
        private Role _role = new Role();

        protected override void establish_context()
        {
            _role.Name = "System Admin";
        }

        protected override void because()
        {
            using (var session = _sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(_role);

                tx.Commit();
            }
        }

        [Test]
        public void Then_roles_should_be_maintained()
        {
            var role = GetEntityByDescription<Role>("Name", "System Admin");

            Assert.AreEqual("System Admin", role.Name);
        }
    }

    public class When_we_need_different_user_permission : NhibernateSpecBase
    {
        private Permission _permission = new Permission();

        protected override void establish_context()
        {
            _permission.Name = "Customers";
        }

        protected override void because()
        {
            using (var session = _sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(_permission);

                tx.Commit();
            };
        }

        [Test]
        public void Then_permission_should_be_maintained()
        {
            var permission = GetEntityByDescription<Permission>("Name", "Customers");

            Assert.AreEqual("Customers", permission.Name);
        }
    }
}