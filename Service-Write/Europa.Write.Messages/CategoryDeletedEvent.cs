using System;
using Europa.Infrastructure;

namespace Europa.Write.Messages
{
    public class CategoryDeletedEvent : IEvent
    {
        public Guid Id { get; set; }
    }
}