using Autofac;
using Europa.Infrastructure;

namespace Europa.Search.Handlers
{
    public class HandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Event Handlers
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces();

            // Query Handlers
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces();
        }
    }
}