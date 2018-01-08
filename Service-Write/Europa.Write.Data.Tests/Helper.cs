using System;
using Europa.Database;
using Europa.Infrastructure;
using NDbUnit.Core;
using NDbUnit.Postgresql;
using Dapper;

namespace Europa.Write.Data.Tests
{
    public class Helper : IDisposable
    {
        private readonly IConfiguration _connection;
        private INDbUnitTest _ndbUnitTest;

        public Helper(IConfiguration connection)
        {
            _connection = connection;

            // Setup dapper
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
        }

        public void Setup(string data)
        {
            Migrate.Up(_connection.ConnectionString);

            _ndbUnitTest = new PostgresqlDbUnitTest(_connection.ConnectionString);
            _ndbUnitTest.ReadXmlSchema("Files/Schema.xsd");
            if (!string.IsNullOrEmpty(data))
            {
                _ndbUnitTest.ReadXml(data);
            }

            _ndbUnitTest.PerformDbOperation(DbOperationFlag.CleanInsertIdentity);
        }

        public void Dispose()
        {
            _ndbUnitTest.PerformDbOperation(DbOperationFlag.DeleteAll);
            _ndbUnitTest.Dispose();

            Migrate.Down(_connection.ConnectionString);
        }

        public const string IntegrationTests = "IntegrationTests";
    }
}
