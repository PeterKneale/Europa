using System.Data;
using Dapper;
using DB = Europa.Database.DatabaseSchema;

namespace Europa.Write.Data.DataSources
{
    public interface ITagDataSource : IDataSource<Tag>
    {
        Tag Get(string name);
        bool Exists(string name);
        Tag Ensure(string name);
    }

    public class TagDataSource : DataSource<Tag>, ITagDataSource
    {
        public TagDataSource(IDbConnection connection, IDbTransaction transaction = null) : base(connection, transaction)
        {
        }

        public Tag Ensure(string name)
        {
            var tag = Get(name);
            if (tag != null)
            {
                return tag;
            }
            tag = new Tag { Name = name };
            Save(tag);
            return tag;
        }
        
        public bool Exists(string name)
        {
            return Get(name) != null;
        }

        public Tag Get(string name)
        {
            var query = $"select * from {DB.TableTag} where {DB.ColumnName} = @name";
            return Connection.QueryFirstOrDefault<Tag>(query, new { name });
        }
    }
}
