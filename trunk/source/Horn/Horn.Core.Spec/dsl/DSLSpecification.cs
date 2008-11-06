using Horn.Core.dsl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Horn.Core.Spec.dsl
{
    public class When_Horn_Receives_A_Request_For_A_Component : BaseDSLSpecification
    {
        [Test]
        public void Then_Horn_Returns_The_Component_MetaData()
        {
            var instance = factory.Create<BaseConfigReader>(@"boo/projects/hornconfig.boo");

            instance.Prepare();

            AssertHornMetaData(instance);
        }

        private void AssertHornMetaData(BaseConfigReader reader)
        {
            Assert.That(reader, Is.Not.Null);

            Assert.That(reader.InstallName, Is.EqualTo("horn"));

            Assert.That(reader.Description, Is.EqualTo("This is a description of horn"));

            Assert.That(reader.Svn, Is.EqualTo("https://svnserver/trunk"));

            Assert.That(reader.BuildTasks[0], Is.EqualTo("one"));

            Assert.That(reader.BuildTasks[1], Is.EqualTo("two"));

            Assert.That(reader.BuildTasks[2], Is.EqualTo("three"));
        }
    }
}
