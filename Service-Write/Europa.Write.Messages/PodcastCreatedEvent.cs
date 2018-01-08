using System;
using Europa.Infrastructure;

namespace Europa.Write.Messages
{
    public class PodcastCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
    }
}