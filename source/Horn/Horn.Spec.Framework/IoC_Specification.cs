using System;
using System.IO;
using Castle.Windsor;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Rhino.Commons.Binsor;

namespace Horn.Spec.Framework
{
    public abstract class IoC_Specification : Specification
    {
        protected IWindsorContainer container;

        public virtual void CreateContainer(string booFilename, bool createDatabase)
        {
            container = new WindsorContainer();
            
            var binsorFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, booFilename));

            BooReader.Read(container, binsorFilePath);

            if (createDatabase) CreateDatabase();
        }

        protected void CreateDatabase()
        {
            var cfg = container.Resolve<Configuration>();
            var export = new SchemaExport(cfg);
            export.Execute(true, true, false, true);
        }
    }
}