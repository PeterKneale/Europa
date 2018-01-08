using System;
using Europa.Infrastructure;

namespace Europa.Write.Messages
{
    public class PodcastDeletedEvent : IEvent
    {
        public Guid Id { get; set; }
    }
}