using System.Data;
using core.domain;
using MbUnit.Framework;
using NHibernate.Cfg;

namespace core.tests.cfg
{
    [TestFixture]
    public class When_nhibernate_is_configured_correctly
    {


        [Test]
        public void The_we_can_open_a_session()
        {
            var sessionFactory = new Configuration().AddAssembly(typeof (User).Assembly).BuildSessionFactory();

            using(var session = sessionFactory.OpenSession())
            {
                Assert.AreEqual(session.Connection.State, ConnectionState.Open);        
            }
        }
    }
}