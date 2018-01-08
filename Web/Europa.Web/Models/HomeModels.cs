using System;
using System.Collections.Generic;

namespace Europa.Web.Models
{
    public class HomePageModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}