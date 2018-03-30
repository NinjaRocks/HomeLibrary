using Autofac;

namespace MicroFx.Data.EntityFramework
{
    public class EntityFrameworkAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           builder.RegisterType(typeof(UnitOfWork))
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