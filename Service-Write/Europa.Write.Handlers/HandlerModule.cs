using Autofac;
using Europa.Infrastructure;

namespace Europa.Write.Handlers
{
    public class HandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Command Handlers
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces();
        }
    }
}
