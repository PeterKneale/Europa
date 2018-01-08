using Autofac;
using Europa.Feed.Handlers;
using Europa.Infrastructure;
using System;

namespace Europa.Feed.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildContainer().Resolve<App>().Run();
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterModule<HandlerModule>()
                .RegisterModule<BusModule>()
                .RegisterModule<LogModule>();
            builder.RegisterType<App>();
            return builder.Build();
        }
    }
}
