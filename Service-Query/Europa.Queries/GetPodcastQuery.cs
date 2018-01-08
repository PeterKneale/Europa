using System;
using Europa.Infrastructure;
using Europa.Query.Messages.Models;

namespace Europa.Query.Messages
{
    public class GetPodcastQuery : IQuery
    {
        public Guid Id { get; set; }
    }

    public class GetPodcastQueryResult : IQueryResult
    {
        public Podcast Podcast { get; set; }
    }
}
