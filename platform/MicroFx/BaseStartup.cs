using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using AutoMapper.Configuration;
using log4net.Config;
using MicroFx.Configuration.AutoMapper;
using MicroFx.Configuration.Http;
using MicroFx.Configuration.Swagger;
using MicroFx.Extensibility;
using MicroFx.Logging;
using Owin;
using Swashbuckle.Application;
using Module = Autofac.Module;

namespace MicroFx
{
    public abstract class BaseStartup
    {
        protected HttpConfiguration Config;
        private readonly ExtensionModules extensionModules;

        private static readonly ILogger logger = LogManager.GetLogger(typeof(BaseStartup));

        protected BaseStartup(HttpConfiguration config)
        {
            Config = config;
            extensionModules = new ExtensionModules();
        }
     
        protected void UseExtension(IExtensionModule extensionModule)
        {
            extensionModules.Add(extensionModule);
        }
        public virtual void Configuration(IAppBuilder app)
        {
            ConfigureLog4Net();
            ConfigureSwagger();
            ConfigureWebApiRoutes();
            ConfigureContainer(app);
            ConfigureAutomapper();
        }

        private static void ConfigureLog4Net()
        {
            XmlConfigurator.Configure();
        }

        private void ConfigureSwagger()
        {
            var assembly = this.GetType().Assembly;

            var modules = AssemblyHelper.Scan<ISwaggerConfig>(assembly);
            if (!modules.Any()) return;

            var module = modules[0];

            Config
              .EnableSwagger(module.ConfigureSwagger)
              .EnableSwaggerUi(module.ConfigureSwaggerUi);
        }

        private void ConfigureWebApiRoutes()
        {
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            Config.Filters.Add(new AuthorizeAttribute());

            // Web API routes
            Config.MapHttpAttributeRoutes();
                      

            Config.Routes.MapHttpRoute(
                name: "Default", // Route name
                routeTemplate: "{version}/{controller}/{action}/{id}", // URL with parameters
                defaults: new { version = "v1", controller = "Home", action = "Index", id = RouteParameter.Optional } // Parameter defaults
                );

            Config.Formatters.Remove(Config.Formatters.XmlFormatter);
            Config.Formatters.Add(Config.Formatters.JsonFormatter);
            //config.MessageHandlers.Add();


            var assembly = this.GetType().Assembly;
            var modules = AssemblyHelper.Scan<IRouteConfig>(assembly);
            modules.ForEach(m => m.Configure(Config));

            Config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            logger.Info("Configure web api completed.....");
        }

        private void ConfigureContainer(IAppBuilder app)
        {
            var assembly = this.GetType().Assembly;

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(assembly)
                .InstancePerLifetimeScope();

            var modules = AssemblyHelper.Scan<Module>(assembly);
            modules.ForEach(m=>builder.RegisterModule(m));

            builder.RegisterAssemblyModules(assembly);

            extensionModules.Register(new RegisterContext(builder));

            var container = builder.Build();

            var dependencyResolver = new AutofacWebApiDependencyResolver(container);
            Config.DependencyResolver = dependencyResolver;

            // add lifetimescope to owin context.
            app.UseAutofacMiddleware(container);
            // add lifetimescope from owin to web api scope
            app.UseAutofacWebApi(Config);

            IoC.Initialize(container);

            app.UseWebApi(Config);

            logger.Info("Configure container completed.....");
        }
        public void ConfigureAutomapper()
        {
            var assembly = this.GetType().Assembly;

            var modules = AssemblyHelper.Scan<IMapperModule>(assembly);
            modules.ForEach(t => t.Load());
            
            var modules1 = AssemblyHelper.Scan<Profile>(assembly);

            modules1.ForEach(t =>
            {
                var configs = new MapperConfigurationExpression();
                configs.AddProfile(t);
                Mapper.Initialize(configs);
            });

            logger.Info("Configure automapper completed.....");
        }
    }
}