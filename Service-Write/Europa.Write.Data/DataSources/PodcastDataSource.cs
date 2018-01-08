using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Europa.Write.Data.DataSources
{
    public interface IPodcastDataSource : IDataSource<Podcast>
    {
        IEnumerable<Podcast> ListByCategory(Guid categoryId);
    }

    public class PodcastDataSource : DataSource<Podcast>, IPodcastDataSource
    {
        public PodcastDataSource(IDbConnection connection, IDbTransaction transaction = null) : base(connection, transaction)
        {
        }
        
        public IEnumerable<Podcast> ListByCategory(Guid categoryId)
        {
            return Connection.GetList<Podcast>(new { CategoryId = categoryId }, Transaction);
        }
    }
}
