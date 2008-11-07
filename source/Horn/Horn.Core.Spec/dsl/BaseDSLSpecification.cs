namespace Horn.Core.Spec.dsl
{
    using System;
    using Core.dsl;
    using Horn.Spec.Framework;
    using Rhino.DSL;

    public abstract class BaseDSLSpecification : Specification
    {
        protected DslFactory factory;

        protected override void Before_each_spec()
        {
            factory = new DslFactory { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory };
            factory.Register<BaseConfigReader>(new ConfigReaderEngine());
        }
    }
}