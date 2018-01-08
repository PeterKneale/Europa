using System;
using System.Collections;
using System.Collections.Generic;

namespace Europa.Web.Models
{
    public class PodcastPageModel
    {
        public CategoryViewModel Category { get; set; }
        public PodcastViewModel Podcast { get; set; }
    }

    public class PodcastViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }

    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PodcastCount { get; set; }
    }
}