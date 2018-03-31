using Autofac;

namespace MicroFx.Data.EntityFramework
{
    public class EntityFrameworkAutofacModule : Module
    {
        private readonly IDbConnectionProvider dbConnectionProvider;

        public EntityFrameworkAutofacModule(IDbConnectionProvider dbConnectionProvider)
        {
            this.dbConnectionProvider = dbConnectionProvider;
        }

        protected override void Load(ContainerBuilder builder)
        {
           builder.RegisterType(typeof(UnitOfWork))
                .WithParameter(new NamedParameter("dbConnectionProvider", dbConnectionProvider))
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