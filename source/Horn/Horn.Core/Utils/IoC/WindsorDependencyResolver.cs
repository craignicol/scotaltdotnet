using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Horn.Core.dsl;
using Horn.Core.Package;

namespace Horn.Core.Utils.IoC
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly WindsorContainer innerContainer;

        public T Resolve<T>()
        {
            return innerContainer.Resolve<T>();
        }

        public T Resolve<T>(string key)
        {
            return innerContainer.Resolve<T>(key);
        }

        public WindsorDependencyResolver()
        {
            innerContainer = new WindsorContainer();

            innerContainer.Register(
                Component.For<IBuildConfigReader>()
                            .Named("buildconfigreader")
                            .ImplementedBy<BuildConfigReader>()
                            .LifeStyle.Transient
                            );

            innerContainer.Register(
                Component.For<SVNSourceControl>()
                            .Named("svn")
                            .LifeStyle.Transient
                );

            innerContainer.Register(
                Component.For<IPackageCommand>()
                            .Named("install")
                            .ImplementedBy<PackageBuilder>()
                            .LifeStyle.Transient
                );
        }
    }
}