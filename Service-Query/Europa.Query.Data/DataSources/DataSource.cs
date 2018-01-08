using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Europa.Query.Data.DataSources
{
    public interface IDataSource<T> where T : class, IData
    {
        IEnumerable<T> List();
        T Get(Guid id);
        bool Exists(Guid id);
    }
    
    public class DataSource<T> : IDataSource<T> where T : class, IData
    {
        protected IDbConnection Connection;

        public DataSource(IDbConnection connection)
        {
            Connection = connection;
        }

        public T Get(Guid id)
        {
            return Connection.Get<T>(id);
        }

        public IEnumerable<T> List()
        {
            return Connection.GetList<T>();
        }

        public bool Exists(Guid id)
        {
            return Get(id) != null;
        }
    }
}