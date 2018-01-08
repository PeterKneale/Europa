using System;

namespace Europa.Query.Messages.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PodcastCount { get; set; }
    }
}