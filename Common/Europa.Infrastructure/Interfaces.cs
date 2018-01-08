using System.Threading.Tasks;

namespace Europa.Infrastructure
{
    // Based on http://web-matters.blogspot.com.au/2014/08/cqrs-with-aspnet-mvc-entity-framework.html

    public interface IMessage
    {

    }
    public interface IEvent : IMessage
    {
    }

    public interface ICommand : IMessage
    {
    }

    public interface IQuery : IMessage
    {
    }

    public interface IQueryResult : IMessage
    {
    }


    public interface IHandler
    {

    }

    public interface ICommandHandler<in T> : IHandler
        where T : ICommand
    {
        Task Execute(T command);
    }

    public interface IEventHandler<in T> : IHandler
        where T : IEvent
    {
        Task Handle(T evnt);
    }

    // Interface for query handlers - has two type parameters for the query and the query result
    public interface IQueryHandler<in TQuery, TResult> : IHandler
       where TResult : IQueryResult
       where TQuery : IQuery
    {
        Task<TResult> Execute(TQuery query);
    }
}