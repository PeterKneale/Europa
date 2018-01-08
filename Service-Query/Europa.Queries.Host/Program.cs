using Autofac;
using Europa.Infrastructure;
using Europa.Query.Data;
using Europa.Query.Handlers;
using IContainer = Autofac.IContainer;

namespace Europa.Query.Host
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
                .RegisterModule<DataModule>()
                .RegisterModule<LogModule>();
            builder.RegisterType<App>();
            return builder.Build();
        }
    }
}
