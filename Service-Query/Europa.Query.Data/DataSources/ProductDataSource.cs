using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Europa.Query.Data.DataSources
{
    public interface IPodcastDataSource
    {
        PodcastData Get(Guid id);
        IEnumerable<PodcastData> List();
        IEnumerable<PodcastData> ListByCategory(Guid id);
    }

    public class PodcastDataSource : DataSource<PodcastData>, IPodcastDataSource
    {
        public PodcastDataSource(IDbConnection connection) : base(connection)
        {
        }
        
        public IEnumerable<PodcastData> ListByCategory(Guid id)
        {
            return Connection.GetList<PodcastData>(new {CategoryId = id});
        }
    }
}