using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Horn.Core.dsl;
using Horn.Core.Get;
using Horn.Core.PackageCommands;
using Horn.Core.SCM;

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

            //TODO: Scan the horn.core assembly and add the components automatically
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

            innerContainer.Register(
                Component.For<IGet>()
                            .Named("get")
                            .ImplementedBy<Get.Get>()
                            .LifeStyle.Transient
                );

            innerContainer.Register(
                Component.For<IFileSystemProvider>()
                            .Named("filesystemprovider")
                            .ImplementedBy<FileSystemProvider>()
                            .LifeStyle.Transient
                );
        }
    }
}