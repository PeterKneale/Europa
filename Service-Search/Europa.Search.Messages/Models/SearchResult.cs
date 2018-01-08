using System;

namespace Europa.Search.Messages.Models
{
    public class SearchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string[] Tags { get; set; }
    }
}