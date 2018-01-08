using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace Europa.Infrastructure

{
    public interface ICommandExecutor
    {
        Task ExecuteCommand<T>(T command) where T : ICommand;
    }

    public interface IEventExecutor
    {
        Task ExecuteEvent<T>(T evnt) where T : IEvent;
    }

    public interface IQueryExecutor
    {
        Task<TResult> ExecuteQuery<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : IQueryResult;
    }

    public class ExecutorLogger
    {
    }
    public class Executor : ICommandExecutor, IEventExecutor, IQueryExecutor
    {
        private readonly IComponentContext _resolver;
        private readonly ILogger<Executor> _log;

        public Executor(IComponentContext resolver, ILogger<Executor> log)
        {
            _resolver = resolver;
            _log = log;
        }

        public async Task ExecuteCommand<T>(T command) where T : ICommand
        {
            AnnounceExecuting(command);

            if (!_resolver.IsRegistered<ICommandHandler<T>>())
            {
                AnnounceNoHandlerFound(command);
                throw new Exception($"No handler could be found for {command.GetType().Name}");
            }

            var handler = _resolver.Resolve<ICommandHandler<T>>();

            AnnounceHandlerFound(command, handler);

            var stopwatch = Stopwatch.StartNew();
            try
            {
                await handler.Execute(command);
            }
            catch (Exception e)
            {
                AnnounceFailed(handler, command, e);
                throw;
            }
            stopwatch.Stop();

            AnnounceExecuted(command, handler, stopwatch.ElapsedMilliseconds);
        }
        
        public async Task<TResult> ExecuteQuery<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : IQueryResult
        {
            AnnounceExecuting(query);

            if (!_resolver.IsRegistered<IQueryHandler<TQuery, TResult>>())
            {
                AnnounceNoHandlerFound(query);
                throw new Exception($"No handler could be found for {query.GetType().Name}");
            }

            var handler = _resolver.Resolve<IQueryHandler<TQuery, TResult>>();

            AnnounceHandlerFound(query, handler);

            var stopwatch = Stopwatch.StartNew();
            TResult result;
            try
            {
                result = await handler.Execute(query);
            }
            catch (Exception e)
            {
                AnnounceFailed(handler, query, e);
                throw;
            }

            stopwatch.Stop();

            AnnounceExecuted(query, handler, stopwatch.ElapsedMilliseconds);

            AnnounceResponse(query, result);

            return result;
        }

        public async Task ExecuteEvent<T>(T evnt) where T : IEvent
        {
            AnnounceExecuting(evnt);

            if (!_resolver.IsRegistered<IEventHandler<T>>())
            {
                AnnounceNoHandlerFound(evnt);
                return;
            }

            var handlers = _resolver.Resolve<IEnumerable<IEventHandler<T>>>();
            foreach(var handler in handlers)
            {
                AnnounceHandlerFound(evnt, handler);

                var stopwatch = Stopwatch.StartNew();
                try
                {
                    await handler.Handle(evnt);
                }
                catch (Exception e)
                {
                    AnnounceFailed(handler, evnt, e);
                    throw;
                }
                stopwatch.Stop();

                AnnounceExecuted(evnt, handler, stopwatch.ElapsedMilliseconds);
            }
        }

        public void AnnounceExecuting(IMessage message)
        {
            var name = message.GetType().Name;
            var payload = JsonConvert.SerializeObject(message);
            _log.LogInformation($"Executing {name}");
            _log.LogDebug($"Executing {name} with body {payload}");
        }

        protected void AnnounceResponse(IMessage query, IMessage response)
        {
            var name = query.GetType().Name;
            var messageQuery = JsonConvert.SerializeObject(query);
            var messageResponse = JsonConvert.SerializeObject(response);
            _log.LogDebug($"Response to {name} with query {messageQuery} is {messageResponse}");
        }

        protected void AnnounceHandlerFound(IMessage message, IHandler handler)
        {
            var messageName = message.GetType().Name;
            var handlerName = handler.GetType().Name;
            _log.LogInformation($"Found handler {handlerName} for {messageName}");
        }

        protected void AnnounceExecuted(IMessage message, IHandler handler, long ms)
        {
            var messageName = message.GetType().Name;
            var handlerName = handler.GetType().Name;
            _log.LogInformation($"Handler {handlerName} executed {messageName}. Duration: {ms}ms.");
        }

        protected void AnnounceFailed(IHandler handler, IMessage message, Exception ex)
        {
            var messageName = message.GetType().Name;
            var handlerName = handler.GetType().Name;
            _log.LogError($"Handler {handlerName} executed {messageName} and threw exception.{ex.Message}. {ex.StackTrace}");
        }

        protected void AnnounceNoHandlerFound(IMessage message)
        {
            var messageName = message.GetType().Name;
            _log.LogError($"No handler could be foud for {messageName}.");
        }
    }
}