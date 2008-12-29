using System;
using Horn.Core.dsl;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;
using Rhino.DSL;
using Rhino.Mocks;

namespace Horn.Core.Spec.Unit.dsl
{
    public class BuildWithNantSpecificationBase : Specification
    {
        protected BaseConfigReader configReader;

        protected DslFactory factory;
        protected IDependencyResolver dependencyResolver;

        protected IPackageTree packageTree;

        protected override void Before_each_spec()
        {
            dependencyResolver = CreateStub<IDependencyResolver>();
            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>()).Return(new SVNSourceControl(string.Empty));

            IoC.InitializeWith(dependencyResolver);

            factory = new DslFactory { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory };
            factory.Register<BaseConfigReader>(new ConfigReaderEngine());

            packageTree = MockRepository.GenerateStub<IPackageTree>();
        }

        protected override void Because()
        {
            throw new System.NotImplementedException();
        }
    }
}