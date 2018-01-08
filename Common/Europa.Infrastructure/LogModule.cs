using System.Data;
using Autofac;
using Microsoft.Extensions.Logging;

namespace Europa.Infrastructure
{
    public class LogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var factory = new LoggerFactory()
                .AddConsole(LogLevel.Debug)
                .AddDebug();
            
            builder.RegisterInstance(factory).As<ILoggerFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

            // TODO: Move to its own module
            builder.RegisterType<Configuration>().As<IConfiguration>().SingleInstance();
            builder.RegisterType<DbConnectionFactory>().As<IDbConnectionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<IDbConnectionFactory>().Create()).As<IDbConnection>().InstancePerLifetimeScope();
        }
    }
}
