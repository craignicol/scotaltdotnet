using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Horn.Core.dsl;

namespace Horn.Core.Utils.IoC
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly WindsorContainer innerContainer;

        public T Resolve<T>()
        {
            return innerContainer.Resolve<T>();
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
        }
    }
}