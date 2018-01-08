using Autofac;
using Europa.Infrastructure;

namespace Europa.Query.Handlers
{
    public class HandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Query Handlers
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces();
        }
    }
}
