using System;
using System.ComponentModel;
using MbUnit.Framework;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace core.tests.SchemaGen
{
    [TestFixture]
    public class A_SchemaGen_Fixture
    {
        [Ignore]
        public void GenerateSchema()
        {
            var cfg = new Configuration()
                        .AddAssembly(typeof(Blank).Assembly);

            var export = new SchemaExport(cfg);

            export.Drop(true, true);

            export.Create(true, true);
        }

        [Test]
        public virtual void GenerateUpdate()
        {
            try
            {
                var cfg = new Configuration()
                        .AddAssembly(typeof(Blank).Assembly);

                var schemaUpdate = new SchemaUpdate(cfg);

                schemaUpdate.Execute(true, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }
    }
}