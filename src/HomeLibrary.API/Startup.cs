using System.Web.Http;
using MicroFx;
using MicroFx.Data.EntityFramework;
using MicroFx.Logging;
using Owin;

namespace HomeLibrary.API
{
    public class Startup: BaseStartup
    {
        private static readonly ILogger logger = LogManager.GetLogger(typeof(Startup));

        public override void Configuration(IAppBuilder app)
        {
            app.UseWelcomePage("/");
            base.Configuration(app);
        }

        public Startup() : base(new HttpConfiguration())
        {
            logger.Info("Startup started....");
            UseExtension(new EntityFrameworkModule(new DbConnectionProvider()));
        }
    }
}