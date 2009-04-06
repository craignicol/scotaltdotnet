using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Horn.Core.Dsl;
using Horn.Core.GetOperations;
using Horn.Core.PackageCommands;
using Horn.Domain.BuildEngines;
using Horn.Domain.Dsl;
using Horn.Domain.SCM;


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
                            .Named("boo")
                            .ImplementedBy<BooBuildConfigReader>()
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
        }
    }
}