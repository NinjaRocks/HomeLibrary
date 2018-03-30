using MicroFx.Bus.Configuration;

namespace HomeLibrary.API.Host
{
    [ServiceBusConfiguration]
    public class ServiceBusConfig: IBusConfiguration
    {
        public void Configure(IConfigContext context)
        {
            
        }
    }
}
