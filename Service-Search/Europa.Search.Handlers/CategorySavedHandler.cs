using System.Linq;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Messages;
using Europa.Search.Index;
using Europa.Write.Messages;

namespace Europa.Search.Handlers
{
    public class CategorySavedHandler : IEventHandler<CategoryCreatedEvent>
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IIndexer _indexer;

        public CategorySavedHandler(IQueryDispatcher queryDispatcher, IIndexer indexer)
        {
            _queryDispatcher = queryDispatcher;
            _indexer = indexer;
        }

        public async Task Handle(CategoryCreatedEvent evnt)
        {
            var id = evnt.Id;

            var result = await _queryDispatcher.Request<GetPodcastsQuery, GetPodcastsQueryResult>(new GetPodcastsQuery { CategoryId = id });
            
            var podcasts = result.Podcasts;
            foreach(var podcast in podcasts)
            {
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
}
