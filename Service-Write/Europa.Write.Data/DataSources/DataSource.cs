using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Europa.Write.Data.DataSources
{
    public interface IDataSource<T> where T : class, IData
    {
        void Save(T item);
        IEnumerable<T> List();
        T Get(Guid id);
        bool Exists(Guid id);
        void Delete(Guid id);
        void Update(T item);
    }

    public class DataSource<T> : IDataSource<T> where T : class, IData
    {
        protected IDbTransaction Transaction;
        protected IDbConnection Connection;

        public DataSource(IDbConnection connection, IDbTransaction transaction = null)
        {
            Connection = connection;
            Transaction = transaction;
        }

        public void Save(T item)
        {
            item.Id = Connection.Insert<Guid, T>(item, Transaction);
        }
        
        public void Update(T item)
        {
            Connection.Update(item, Transaction);
        }

        public void Delete(Guid id)
        {
            Connection.Delete<T>(id, Transaction);
        }

        public T Get(Guid id)
        {
            return Connection.Get<T>(id, Transaction);
        }

        public IEnumerable<T> List()
        {
            return Connection.GetList<T>(null, transaction: Transaction);
        }

        public bool Exists(Guid id)
        {
            return Get(id)!=null;
        }
    }
}
