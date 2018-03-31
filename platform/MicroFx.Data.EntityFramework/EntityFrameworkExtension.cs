using System.Reflection;
using Autofac;
using MicroFx.Extensibility;
using MicroFx.Logging;

namespace MicroFx.Data.EntityFramework
{
    public class EntityFrameworkModule: BaseExtensionModule
    {
        private readonly IDbConnectionProvider dbConnectionProvider;
        private static readonly ILogger logger = LogManager.GetLogger(typeof(EntityFrameworkModule));

        public EntityFrameworkModule(IDbConnectionProvider dbConnectionProvider)
        {
            this.dbConnectionProvider = dbConnectionProvider;
        }

        public override bool Register(IRegisterContext context)
        {
            context.builder.RegisterModule(new DataAutofacModule(dbConnectionProvider));
            context.builder.RegisterModule(new EntityFrameworkAutofacModule(dbConnectionProvider));

            base.Next(context);

            logger.Info("EF Module initialed...");
            return true;
        }
    }
}
