﻿namespace Horn.Core.Spec.dsl
{
    using Core.dsl;
    using Xunit;

    public class When_Horn_Receives_A_Request_For_A_Component : BaseDSLSpecification
    {
        private BaseConfigReader instance;

        protected override void Because()
        {
            instance = factory.Create<BaseConfigReader>(@"boo/projects/hornconfig.boo");
            instance.Prepare();
        }

        [Fact]
        public void Then_Horn_Returns_The_Component_MetaData()
        {
            AssertHornMetaData(instance);
        }

        private void AssertHornMetaData(BaseConfigReader reader)
        {
            Assert.NotNull(reader);

            Assert.Equal("horn", reader.InstallName);

            Assert.Equal("This is a description of horn", reader.Description);

            Assert.Equal("https://svnserver/trunk", reader.Svn);

            //Assert.That(reader.BuildTasks[0], Is.EqualTo("one"));

            //Assert.That(reader.BuildTasks[1], Is.EqualTo("two"));

            //Assert.That(reader.BuildTasks[2], Is.EqualTo("three"));
        }
    }
}
