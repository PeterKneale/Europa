using System;
using System.Data;
using Europa.Infrastructure;
using Europa.Write.Data.DataSources;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Europa.Write.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
        IPodcastDataSource Podcasts { get; }
        ICategoryDataSource Categories { get; }
        ITagDataSource Tags { get; }
        IPodcastTagDataSource PodcastTags { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection;
        private readonly ILogger<UnitOfWork> _log;

        public UnitOfWork(IConfiguration configuration, ILogger<UnitOfWork> log)
        {
            _log = log;
            Connect(configuration);
        }

        private void Connect(IConfiguration connection)
        {
            try
            {
                _connection = new NpgsqlConnection(connection.ConnectionString);
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex, $"Unable to connect to database. {ex.Message}");
                throw;
            }
        }

        public void Begin()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _log.LogDebug("Opening connection.");
                _connection.Open();
            }

            _log.LogDebug("Begin transaction.");
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _log.LogDebug("Commit transaction.");
            _transaction.Commit();
        }

        public void Rollback()
        {
            _log.LogWarning("Rollback Transaction.");
            _transaction.Rollback();
        }

        public IPodcastDataSource Podcasts => new PodcastDataSource(_connection, _transaction);

        public ICategoryDataSource Categories => new CategoryDataSource(_connection, _transaction);

        public ITagDataSource Tags => new TagDataSource(_connection, _transaction);

        public IPodcastTagDataSource PodcastTags => new PodcastTagDataSource(_connection, _transaction);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
            if (_connection == null)
            {
                return;
            }
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            _connection = null;
        }
    }
}
