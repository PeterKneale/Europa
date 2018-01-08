using System.Collections.Generic;
using SolrNet.Attributes;

namespace Europa.Search.Index
{
    public class PodcastDocument
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrField("category")]
        public string Category { get; set; }

        [SolrField("tags")]
        public ICollection<string> Tags { get; set; }

        [SolrField("title")]
        public string Title { get; set; }
    }
}
