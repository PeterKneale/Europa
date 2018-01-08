using Autofac;
using Dapper;
using EasyNetQ;
using Europa.Database;
using Europa.Infrastructure;
using Europa.Write.Messages;
using Microsoft.Extensions.Logging;

namespace Europa.Write.Host
{
    public class App
    {
        private readonly IBus _bus;
        private readonly IConfiguration _dbConfig;
        private readonly ILogger<App> _logger;
        private readonly IComponentContext _resolver;

        public App(IBus bus, IConfiguration dbConfig, ILogger<App> logger, IComponentContext resolver)
        {
            _bus = bus;
            _dbConfig = dbConfig;
            _logger = logger;
            _resolver = resolver;
        }

        public void Run()
        {
            _logger.LogInformation($"Running {typeof(App).FullName}");
            
            // Setup dapper
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            
            // Setup database
            Migrate.Up(_dbConfig.ConnectionString);

            // Setup commands
            Setup<CreateCategoryCommand>();
            Setup<CreatePodcastCommand>();
            Setup<DeleteCategoryCommand>();
            Setup<DeletePodcastCommand>();
            Setup<PopulateCommand>();
        }

        private void Setup<TCommand>()
            where TCommand : class, ICommand
        {
            _logger.LogInformation($"Setting up to handle command {typeof(TCommand).Name}");
            _bus.Receive<TCommand>(typeof(TCommand).Name, request => _resolver.Resolve<ICommandExecutor>().ExecuteCommand(request));
        }
    }
}