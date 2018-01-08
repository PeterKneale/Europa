using System;
using Europa.Infrastructure;
using Europa.Feed.Messages.Models;

namespace Europa.Feed.Messages
{
    public class PodcastRetrievedEvent : IEvent
    {
        public Guid Id { get; set; }
        public Podcast Podcast { get; set; }
    }


}
