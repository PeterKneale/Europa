using System.Collections.Generic;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Data;
using Europa.Query.Data.DataSources;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;

namespace Europa.Query.Handlers
{
    public class GetPodcastsQueryHandler : IQueryHandler<GetPodcastsQuery, GetPodcastsQueryResult>
    {
        private readonly IPodcastDataSource _dataSource;

        public GetPodcastsQueryHandler(IPodcastDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Task<GetPodcastsQueryResult> Execute(GetPodcastsQuery query)
        {
            var data = query.CategoryId.HasValue
                ? _dataSource.ListByCategory(query.CategoryId.Value)
                : _dataSource.List();
            
            var models = AutoMapper.Mapper.Map<IEnumerable<PodcastData>, IEnumerable<Podcast>>(data);
            return Task.FromResult(new GetPodcastsQueryResult { Podcasts = models });
        }
    }
}