using System.Reflection;
using Autofac;
using MicroFx.Extensibility;
using MicroFx.Logging;

namespace MicroFx.Data.EntityFramework
{
    public class EntityFrameworkModule: BaseExtensionModule
    {
        private readonly Assembly callingAssembly;
        private static readonly ILogger logger = LogManager.GetLogger(typeof(EntityFrameworkModule));

        public EntityFrameworkModule(Assembly callingAssembly)
        {
            this.callingAssembly = callingAssembly;
        }

        public override bool Register(IRegisterContext context)
        {
            context.builder.RegisterModule(new DataAutofacModule(callingAssembly));
            context.builder.RegisterModule(new EntityFrameworkAutofacModule());

            base.Next(context);

            logger.Info("EF Module initialed...");
            return true;
        }
    }
}
