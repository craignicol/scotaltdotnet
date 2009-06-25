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
        public void Then_the_details_should_be_saved_to_the_database()
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
            _user = GetEntityByDescription<User>("Email", "dagda1@scotalt.net");

            Assert.AreEqual("dagda1@scotalt.net", _user.Email);
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
}