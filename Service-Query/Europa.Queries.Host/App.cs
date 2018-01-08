using Autofac;
using AutoMapper;
using Dapper;
using EasyNetQ;
using Europa.Infrastructure;
using Europa.Query.Handlers;
using Europa.Query.Messages;
using Microsoft.Extensions.Logging;

namespace Europa.Query.Host
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

            // Setup Automapper
            Mapper.Initialize(x =>
            {
                x.AddProfile<Mappings>();
            });
            Mapper.AssertConfigurationIsValid();
            
            // Setup handlers
            SetupToRespond<GetCategoriesQuery, GetCategoriesQueryResult>();
            SetupToRespond<GetCategoryQuery, GetCategoryQueryResult>();
            SetupToRespond<GetPodcastQuery, GetPodcastQueryResult>();
            SetupToRespond<GetPodcastsQuery, GetPodcastsQueryResult>();
        }

        private void SetupToRespond<TRequest, TResponse>()
            where TRequest : class, IQuery
            where TResponse : class, IQueryResult
        {
            _logger.LogInformation($"Setting up to handle query {typeof(TRequest).Name}");
            _bus.RespondAsync<TRequest, TResponse>(request => _resolver.Resolve<IQueryExecutor>().ExecuteQuery<TRequest, TResponse>(request));
        }
    }
}