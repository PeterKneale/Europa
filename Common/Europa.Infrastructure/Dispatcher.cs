using EasyNetQ;
using FluentValidation.Results;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Europa.Infrastructure
{
    public interface ICommandDispatcher
    {
        Task Send<TCommand>(TCommand command)
            where TCommand : class, ICommand;
    }

    public interface IQueryDispatcher
    {
        Task<TQueryResult> Request<TQuery, TQueryResult>(TQuery query)
            where TQuery : class, IQuery
            where TQueryResult : class, IQueryResult;
    }

    public interface IEventDispatcher
    {
        Task Publish<TEvent>(TEvent evnt)
            where TEvent : class, IEvent;
    }


    // Implementation of the command dispatcher - selects and executes the appropriate command 
    public class Dispatcher : ICommandDispatcher, IQueryDispatcher, IEventDispatcher
    {
        private readonly IValidator _validator;
        private readonly IBus _bus;
        private readonly ILogger<Dispatcher> _log;

        public Dispatcher(IValidator validator, IBus bus, ILogger<Dispatcher> log)
        {
            _bus = bus;
            _log = log;
            _validator = validator;
        }

        public async Task Send<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            Validate(command);

            _log.LogInformation($"Dispatching command {command.GetType().Name}");

            await _bus.SendAsync(command.GetType().Name, command);
        }

        public async Task Publish<TEvent>(TEvent evnt) where TEvent : class, IEvent
        {
            Validate(evnt);

            _log.LogInformation($"Dispatching event {evnt.GetType().Name}");

            await _bus.PublishAsync(evnt);
        }

        public async Task<TQueryResult> Request<TQuery, TQueryResult>(TQuery query)
            where TQuery : class, IQuery
            where TQueryResult : class, IQueryResult
        {
            Validate(query);

            _log.LogInformation($"Dispatching query {query.GetType().Name}");
            return await _bus.RequestAsync<TQuery, TQueryResult>(query);
        }

        private void Validate<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            if (!_validator.TryValidate(message, out ValidationResult result))
            {
                var errors = string.Join(",", result.Errors);
                throw new System.InvalidOperationException(errors);
            }
        }
    }

}
