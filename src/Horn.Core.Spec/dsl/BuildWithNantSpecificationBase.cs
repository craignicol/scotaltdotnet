using System;
using Horn.Core.Dsl;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;
using Horn.Framework.helpers;
using Rhino.DSL;
using Rhino.Mocks;

namespace Horn.Core.Spec.Unit.dsl
{
    public class BuildWithNantSpecificationBase : Specification
    {
        protected BooConfigReader configReader;
        protected DslFactory factory;
        protected IDependencyResolver dependencyResolver;
        protected IPackageTree packageTree;

        protected override void Before_each_spec()
        {
            dependencyResolver = CreateStub<IDependencyResolver>();
            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>()).Return(new SVNSourceControl(string.Empty));

            IoC.InitializeWith(dependencyResolver);

            factory = new DslFactory { BaseDirectory = DirectoryHelper.GetBaseDirectory() };
            factory.Register<BooConfigReader>(new ConfigReaderEngine());

            packageTree = MockRepository.GenerateStub<IPackageTree>();
        }

        protected override void Because()
        {
            throw new System.NotImplementedException();
        }
    }
}