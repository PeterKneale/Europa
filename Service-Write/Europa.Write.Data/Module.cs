using System.Data;
using Autofac;
using Europa.Write.Data.DataSources;

namespace Europa.Write.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>().InstancePerLifetimeScope();
            builder.Register(x => x.Resolve<IUnitOfWorkFactory>().Create()).As<IUnitOfWork>().InstancePerLifetimeScope();
            
            builder.RegisterType<CategoryDataSource>().As<ICategoryDataSource>().InstancePerLifetimeScope();
            builder.RegisterType<PodcastDataSource>().As<IPodcastDataSource>().InstancePerLifetimeScope();
            builder.RegisterType<TagDataSource>().As<ITagDataSource>().InstancePerLifetimeScope();
            builder.RegisterType<PodcastTagDataSource>().As<IPodcastTagDataSource>().InstancePerLifetimeScope();
        }
    }
}
