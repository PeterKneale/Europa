using System;

namespace Europa.Query.Messages.Models
{
    public class Podcast
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string[] Tags { get; set; }
    }
}