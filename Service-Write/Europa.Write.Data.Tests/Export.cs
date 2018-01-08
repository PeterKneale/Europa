using System.Diagnostics;
using Europa.Infrastructure;
using NDbUnit.Postgresql;
using Xunit;

namespace Europa.Write.Data.Tests
{
    public class TestDataGenerator
    {
        [Fact(Skip = "only run this manually to generate xml from database state")]
        public void Generate_xml_from_database_state()
        {
            var connection = new Configuration();
            var test = new PostgresqlDbUnitTest(connection.ConnectionString);
            test.ReadXmlSchema("Files/Schema.xsd");

            var ds = test.GetDataSetFromDb(null);
            Debug.WriteLine(ds.GetXml());
        }
    }
}
