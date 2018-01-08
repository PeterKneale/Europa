using System.Linq;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Messages;
using Europa.Search.Index;
using Europa.Write.Messages;

namespace Europa.Search.Handlers
{
    public class PodcastSavedHandler : IEventHandler<PodcastCreatedEvent>
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IIndexer _indexer;

        public PodcastSavedHandler(IQueryDispatcher queryDispatcher, IIndexer indexer)
        {
            _queryDispatcher = queryDispatcher;
            _indexer = indexer;
        }

        public async Task Handle(PodcastCreatedEvent evnt)
        {
            var id = evnt.Id;

            var result = await _queryDispatcher.Request<GetPodcastQuery, GetPodcastQueryResult>(new GetPodcastQuery { Id = id });
            
            var podcast = result.Podcast;
            
            var podcastDocument = new PodcastDocument
            {
                Id = podcast.Id.ToString(),
                Title = podcast.Title,
                Category = podcast.CategoryName,
                Tags = podcast.Tags
            };

            await _indexer.Update(podcastDocument);
        }
    }
}
