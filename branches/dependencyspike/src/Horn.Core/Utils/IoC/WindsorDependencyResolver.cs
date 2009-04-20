using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Horn.Core.BuildEngines;
using Horn.Core.Dsl;
using Horn.Core.GetOperations;
using Horn.Core.PackageCommands;
using Horn.Core.SCM;

namespace Horn.Core.Utils.IoC
{
    using System.Reflection;
    using Dependencies;

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
            innerContainer.Kernel.Resolver.AddSubResolver(new EnumerableResolver(innerContainer.Kernel));

            innerContainer.Register(
                Component.For<IBuildConfigReader>()
                            .Named("boo")
                            .ImplementedBy<BooBuildConfigReader>()
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

            innerContainer.Register(
                Component.For<IGet>()
                            .Named("get")
                            .ImplementedBy<Get>()
                            .LifeStyle.Transient
                );

            innerContainer.Register(
                Component.For<IFileSystemProvider>()
                            .Named("filesystemprovider")
                            .ImplementedBy<FileSystemProvider>()
                            .LifeStyle.Transient
                );

            innerContainer.Register(
                Component.For<IProcessFactory>()
                            .Named("processfactory")
                            .ImplementedBy<DiagnosticsProcessFactory>()
                            .LifeStyle.Transient

                );

            innerContainer.Register(
                Component.For<IDependencyDispatcher>()
                    .ImplementedBy<DependencyDispatcher>()
                    .LifeStyle.Transient,

                Component.For<IDependentUpdaterExecutor>()
                    .ImplementedBy<DependentUpdaterExecutor>()
                    .LifeStyle.Transient,

                AllTypes.Of<IDependentUpdater>().FromAssembly(Assembly.GetExecutingAssembly())
                    .WithService.FirstInterface().Configure(config => config.LifeStyle.Transient)
                );
        }
    }
}