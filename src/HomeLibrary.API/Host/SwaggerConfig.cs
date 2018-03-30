using MicroFx;
using MicroFx.Configuration.Swagger;
using MicroFx.Logging;
using Swashbuckle.Application;

namespace HomeLibrary.API.Host
{
    public class SwaggerConfig : ISwaggerConfig
    {
        private static readonly ILogger logger = LogManager.GetLogger(typeof(SwaggerConfig));

        public void ConfigureSwagger(SwaggerDocsConfig config)
        {
            config.SingleApiVersion("v1", Service.GetName());

            logger.Debug("Swagger doc config completed ...");
        }
        public void ConfigureSwaggerUi(SwaggerUiConfig config)
        {
            logger.Debug("Swagger ui config completed ...");
        }
    }
}