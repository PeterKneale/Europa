using System;
using Europa.Infrastructure;
using Europa.Query.Messages.Models;

namespace Europa.Query.Messages
{
    public class GetCategoryQuery : IQuery
    {
        public Guid Id { get; set; }
    }

    public class GetCategoryQueryResult : IQueryResult
    {
        public Category Category { get; set; }
    }
}
