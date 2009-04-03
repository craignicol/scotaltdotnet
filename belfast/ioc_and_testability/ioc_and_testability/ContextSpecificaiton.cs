using NUnit.Framework;

namespace ddd.belfast
{
    [TestFixture]
    public abstract class ContextSpecification
    {
        [TestFixtureTearDown]
        public virtual void after_each_Spec()
        {
        }

        [TestFixtureSetUp]
        public virtual void before_each_spec()
        {
        }

        [SetUp]
        public void setup()
        {
            establish_context();
            because();
        }

        [TearDown]
        public void teardown()
        {
            after_each_specification();
        }

        protected abstract void because();
        protected abstract void establish_context();
        protected virtual void after_each_specification()
        {
        }
    }
}