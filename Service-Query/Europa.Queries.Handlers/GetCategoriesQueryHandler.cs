using System.Collections.Generic;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Data;
using Europa.Query.Data.DataSources;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;
using AutoMapper;

namespace Europa.Query.Handlers
{
    public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, GetCategoriesQueryResult>
    {
        private readonly ICategoryDataSource _dataSource;

        public GetCategoriesQueryHandler(ICategoryDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Task<GetCategoriesQueryResult> Execute(GetCategoriesQuery query)
        {
            var data = _dataSource.List();
            var models = Mapper.Map<IEnumerable<CategoryData>, IEnumerable<Category>> (data);
            return Task.FromResult(new GetCategoriesQueryResult { Categories = models });
        }
    }
}