using NUnit.Framework;

namespace boo.scheduler.test
{
    public class When_the_email_template_has_custom_parameters : DslSpecBase
    {
        protected override string BooFileUnderTest
        {
            get { return "parametersblock.boo"; }
        }

        [Test]
        public void Then_we_can_specify_the_parameters_in_the_dsl()
        {
            Assert.That(_task.Parameters.Count, Is.EqualTo(2));

            Assert.That(_task.Parameters[0].Name, Is.EqualTo("DisplayName"));

            Assert.That(_task.Parameters[1].Name, Is.EqualTo("Url"));
        }
    }
}