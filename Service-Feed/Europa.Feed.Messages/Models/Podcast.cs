using System;
using System.Collections.Generic;
using System.Text;

namespace Europa.Feed.Messages.Models
{
    public class Podcast
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public PodcastEpisode[] Episodes { get; set; }
    }

    public class PodcastEpisode
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
}
