using Autofac;
using AutoMapper;
using Dapper;
using EasyNetQ;
using Europa.Infrastructure;
using Europa.Feed.Handlers;
using Europa.Feed.Messages;
using Microsoft.Extensions.Logging;

namespace Europa.Feed.Host
{
    public class App
    {
        private readonly IBus _bus;
        private readonly ILogger<App> _logger;
        private readonly IComponentContext _resolver;

        public App(IBus bus, ILogger<App> logger, IComponentContext resolver)
        {
            _bus = bus;
            _logger = logger;
            _resolver = resolver;
        }

        public void Run()
        {
            _logger.LogInformation($"Running {typeof(App).FullName}");

            // Setup dapper
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);

            // Setup handlers
            Setup<RetrievePodcastCommand>();
        }

        private void Setup<TCommand>()
            where TCommand : class, ICommand
        {
            _logger.LogInformation($"Setting up to handle command {typeof(TCommand).Name}");
            _bus.Receive<TCommand>(typeof(TCommand).Name, request => _resolver.Resolve<ICommandExecutor>().ExecuteCommand(request));
        }
    }
}