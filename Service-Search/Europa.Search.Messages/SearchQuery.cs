using System;
using Europa.Infrastructure;
using System.Collections.Generic;
using Europa.Search.Messages.Models;

namespace Europa.Search.Messages
{
    public class SearchQuery : IQuery
    {
        public string Query { get; set; }
    }

    public class SearchQueryResult : IQueryResult
    {
        public IEnumerable<SearchResult> Results { get; set; }
    }
}
