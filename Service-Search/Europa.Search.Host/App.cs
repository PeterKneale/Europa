using Autofac;
using EasyNetQ;
using Europa.Infrastructure;
using Europa.Search.Messages;
using Europa.Write.Messages;
using Microsoft.Extensions.Logging;

namespace Europa.Search.Host
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
            SetupToHandle<PodcastCreatedEvent>();
            SetupToHandle<CategoryCreatedEvent>();
            SetupToRespond<SearchQuery, SearchQueryResult>();
        }
        
        private void SetupToHandle<TEvent>()
            where TEvent : class, IEvent
        {
            _logger.LogInformation($"Setting up to handle event {typeof(TEvent).Name}");
            _bus.Subscribe<TEvent>(typeof(Program).FullName, evnt => _resolver.Resolve<IEventExecutor>().ExecuteEvent(evnt));
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