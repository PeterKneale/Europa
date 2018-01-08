using System.Collections.Generic;
using Europa.Infrastructure;
using Europa.Query.Messages.Models;

namespace Europa.Query.Messages
{
    public class GetCategoriesQuery : IQuery
    {
        
    }

    public class GetCategoriesQueryResult : IQueryResult
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
