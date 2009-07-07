using System.Data;
using MbUnit.Framework;
using NHibernate.Cfg;

namespace core.tests.cfg
{
    [TestFixture]
    public class When_nhibernate_is_configured_correctly
    {
        [Test]
        public void The_we_can_open_an_session()
        {
            var cfg = new Configuration()
                        .AddAssembly(typeof(Blank).Assembly);

            var sessionFactory = cfg.BuildSessionFactory();

            using(var session = sessionFactory.OpenSession())
            {
                Assert.IsTrue(session.Connection.State == ConnectionState.Open);
            }
        }
    }
}