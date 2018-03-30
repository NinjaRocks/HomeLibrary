using System;
using Autofac;

namespace HomeLibrary.API.Host
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           Console.WriteLine("custom autofac module ...has run");
        }
    }
}
