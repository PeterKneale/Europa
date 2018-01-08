using System;
using Europa.Infrastructure;

namespace Europa.Write.Messages
{
    public class CategoryCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
    }
}