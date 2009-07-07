using core.domain;
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
        protected User _user;

        public override void before_each_spec()
        {
            _sessionFactory = new Configuration().AddAssembly(typeof (User).Assembly).BuildSessionFactory();
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

        protected void SaveOrUpdateEntity(object entity)
        {
            using(var session = _sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(entity);

                tx.Commit();
            }
        }
    }
}