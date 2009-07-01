using MbUnit.Framework;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace core.tests
{
    [TestFixture]
    public abstract class NhibernateSpecBase : ContextSpecification
    {
        protected ISessionFactory _sessionFactory;

        public override void before_each_spec()
        {
            var cfg = new Configuration()
            .AddAssembly(typeof(Blank).Assembly);

            _sessionFactory = cfg.BuildSessionFactory();
        }

        protected T GetEntityByDescription<T>(string propertyName, object value) where T : class
        {
            T ret = null;

            using (var session = _sessionFactory.OpenSession())
            {
                ret = session.CreateCriteria(typeof(T))
                    .Add(Restrictions.Eq(propertyName, value))
                    .UniqueResult<T>();
            }

            return ret;
        }
    }
}