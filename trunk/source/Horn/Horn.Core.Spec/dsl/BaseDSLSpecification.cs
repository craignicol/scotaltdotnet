using System;
using Horn.Core.dsl;
using NUnit.Framework;
using Rhino.DSL;

namespace Horn.Core.Spec.dsl
{
    [TestFixture]
    public class BaseDSLSpecification
    {
        protected DslFactory factory;

        [SetUp]
        protected void Before_each_spec()
        {
            factory = new DslFactory { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory };
            factory.Register<BaseConfigReader>(new ConfigReaderEngine());
        }
    }
}