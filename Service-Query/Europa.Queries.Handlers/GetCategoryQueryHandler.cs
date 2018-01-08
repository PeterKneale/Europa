using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Data;
using Europa.Query.Data.DataSources;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;

namespace Europa.Query.Handlers
{
    public class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, GetCategoryQueryResult>
    {
        private readonly ICategoryDataSource _dataSource;

        public GetCategoryQueryHandler(ICategoryDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Task<GetCategoryQueryResult> Execute(GetCategoryQuery query)
        {
            var data = _dataSource.Get(query.Id);
            var model = AutoMapper.Mapper.Map<CategoryData, Category>(data);
            return Task.FromResult(new GetCategoryQueryResult { Category = model });
        }
    }
}