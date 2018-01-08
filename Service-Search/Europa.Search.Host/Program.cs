using Autofac;
using Europa.Infrastructure;
using Europa.Search.Handlers;
using Europa.Search.Index;
using IContainer = Autofac.IContainer;

namespace Europa.Search.Host
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
                .RegisterModule<SearchModule>()
                .RegisterModule<BusModule>()
                .RegisterModule<LogModule>();
            builder.RegisterType<App>();
            return builder.Build();
        }
    }
}
