using Autofac;
using Europa.Infrastructure;
using Europa.Write.Data;
using Europa.Write.Handlers;
using IContainer = Autofac.IContainer;

namespace Europa.Write.Host
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
                .RegisterModule<DataModule>()
                .RegisterModule<HandlerModule>()
                .RegisterModule<BusModule>()
                .RegisterModule<LogModule>();
            builder.RegisterType<App>();
            return builder.Build();
        }
    }
}
