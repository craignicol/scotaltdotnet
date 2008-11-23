using System.Collections.Generic;
using System.IO;
using Horn.Core.dsl;
using Horn.Core.Get;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;
using Rhino.Mocks;

namespace Horn.Core.Spec.Unit.PackageCommands
{
    public abstract class PackageCommandSpecificationBase : DirectoryStructureSpecificationBase
    {
        protected IDictionary<string, IList<string>> switches = new Dictionary<string, IList<string>>();

        protected IGet get;

        protected IBuildConfigReader buildConfigReader;

        protected PackageTree packageTree;

        protected override void Before_each_spec()
        {
            base.Before_each_spec();

            packageTree = new PackageTree(new DirectoryInfo(root), null);

            IBuildConfigReader buildConfigReader = new BuildConfigReader();

            var dependencyResolver = CreateStub<IDependencyResolver>();

            dependencyResolver.Stub(x => x.Resolve<IBuildConfigReader>()).Return(buildConfigReader);

            var svn = new SVNSourceControl("https://svnserver/trunk");

            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>()).Return(svn);

            IoC.InitializeWith(dependencyResolver);
        }
    }
}