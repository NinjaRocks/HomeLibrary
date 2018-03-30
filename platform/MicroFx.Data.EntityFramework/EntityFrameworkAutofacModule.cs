using Autofac;

namespace MicroFx.Data.EntityFramework
{
    public class EntityFrameworkAutofacModule : Module
    {
        private readonly string dbConnectionString;

        public EntityFrameworkAutofacModule(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
           builder.RegisterType(typeof(UnitOfWork))
                .WithParameter(new NamedParameter("connectionString", dbConnectionString))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}