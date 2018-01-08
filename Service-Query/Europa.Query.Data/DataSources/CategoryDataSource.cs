using System;
using System.Collections.Generic;
using System.Data;

namespace Europa.Query.Data.DataSources
{
    public interface ICategoryDataSource
    {
        CategoryData Get(Guid id);
        IEnumerable<CategoryData> List();
    }

    public class CategoryDataSource : DataSource<CategoryData>, ICategoryDataSource
    {
        public CategoryDataSource(IDbConnection connection) : base(connection)
        {
        }
    }
}