using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;
using Europa.Search.Messages;
using Europa.Search.Index;

namespace Europa.Query.Handlers
{
    public class SearchQueryHandler : IQueryHandler<SearchQuery, SearchQueryResult>
    {
        private readonly ISearcher _search;

        public SearchQueryHandler(ISearcher search)
        {
            _search = search;
        }
        
        public async Task<SearchQueryResult> Execute(SearchQuery query)
        {
            var result = await _search.Search(query.Query);
            return await Task.FromResult(new SearchQueryResult());
        }
    }
}