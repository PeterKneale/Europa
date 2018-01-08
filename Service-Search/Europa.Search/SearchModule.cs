using Autofac;
using AutofacContrib.SolrNet;

namespace Europa.Search.Index
{
    public class SearchModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new SolrNetModule("http://HOST:32768/solr/europa"));
            builder.RegisterType<Indexer>().As<IIndexer>();
            builder.RegisterType<Indexer>().As<ISearcher>();
        }
    }
}