using NUnit.Framework;

namespace core.tests.cfg
{
    [TestFixture]
    public class When_the_application_starts
    {
        [Test]
        public void Then_the_configuration_is_initialised()
        {
            var cfg = ConfigurationHelper.GetDefaultConfiguration();
        }
    }
}