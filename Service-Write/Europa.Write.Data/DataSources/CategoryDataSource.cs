using System.Data;

namespace Europa.Write.Data.DataSources
{
    public interface ICategoryDataSource : IDataSource<Category>
    {

    }

    public class CategoryDataSource : DataSource<Category>, ICategoryDataSource
    {
        public CategoryDataSource(IDbConnection connection, IDbTransaction transaction = null) : base(connection, transaction)
        {
        }
    }
}
