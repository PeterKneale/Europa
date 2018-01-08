using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Europa.Web.Areas.Admin.Models
{
    public class PodcastViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
    
    public class PodcastCreateModel
    {
        public Guid CategoryId { get; set; }

        [Display(Description = "Podcast Link")]
        public string Link { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }

    public class WaitModel
    {
        public Guid Id { get; set; }

        public string Message { get; set; }
    }

    public class PodcastEditModel
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        [Display(Description = "Podcast Title")]
        public string Title { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}