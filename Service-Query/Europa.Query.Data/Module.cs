using Autofac;
using Europa.Query.Data.DataSources;

namespace Europa.Query.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryDataSource>().As<ICategoryDataSource>().InstancePerLifetimeScope();
            builder.RegisterType<PodcastDataSource>().As<IPodcastDataSource>().InstancePerLifetimeScope();
        }
    }
}
