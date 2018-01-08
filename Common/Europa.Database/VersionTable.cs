using FluentMigrator.VersionTableInfo;

namespace Europa.Database
{
    [VersionTableMetaData]
    public class VersionTable : DefaultVersionTableMetaData
    {
        public override string TableName => "version";
    }
}
