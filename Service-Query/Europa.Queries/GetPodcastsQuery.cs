using System;
using System.Collections.Generic;
using Europa.Infrastructure;
using Europa.Query.Messages.Models;

namespace Europa.Query.Messages
{
    public class GetPodcastsQuery : IQuery
    {
        public Guid? CategoryId { get; set; }
    }

    public class GetPodcastsQueryResult : IQueryResult
    {
        public IEnumerable<Podcast> Podcasts { get; set; }
    }
}
