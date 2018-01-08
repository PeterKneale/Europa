using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Data;
using Europa.Query.Data.DataSources;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;

namespace Europa.Query.Handlers
{
    public class GetPodcastQueryHandler : IQueryHandler<GetPodcastQuery, GetPodcastQueryResult>
    {
        private readonly IPodcastDataSource _dataSource;

        public GetPodcastQueryHandler(IPodcastDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Task<GetPodcastQueryResult> Execute(GetPodcastQuery query)
        {
            var data = _dataSource.Get(query.Id);
            var model = AutoMapper.Mapper.Map<PodcastData, Podcast>(data);
            return Task.FromResult(new GetPodcastQueryResult { Podcast = model });
        }
    }
}