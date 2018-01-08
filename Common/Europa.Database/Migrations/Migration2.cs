using System.Data;
using FluentMigrator;

namespace Europa.Database.Migrations
{
    [Migration(2, "Add Tags")]
    public class Migration2 : Migration
    {
        public override void Up()
        {
            Create.Table(DatabaseSchema.TableTag)
                .WithColumn(DatabaseSchema.ColumnId)
                    .AsGuid()
                    .PrimaryKey()
                    .NotNullable()
                .WithColumn(DatabaseSchema.ColumnName)
                    .AsString(DatabaseSchema.ColumnNameLength)
                    .Unique()
                    .NotNullable();

            Create.Table(DatabaseSchema.TablePodcastTag)
                .WithColumn(DatabaseSchema.ColumnId)
                    .AsGuid()
                    .PrimaryKey()
                    .NotNullable()
                .WithColumn(DatabaseSchema.ColumnPodcastId)
                    .AsGuid()
                    .NotNullable()
                    .ForeignKey(DatabaseSchema.TablePodcast, DatabaseSchema.ColumnId)
                    .OnDelete(Rule.Cascade)
                .WithColumn(DatabaseSchema.ColumnTagId)
                    .AsGuid()
                    .NotNullable()
                    .ForeignKey(DatabaseSchema.TableTag, DatabaseSchema.ColumnId)
                    .OnDelete(Rule.Cascade);


            Create.UniqueConstraint(DatabaseSchema.ConstrainUniquePodcastTag)
                .OnTable(DatabaseSchema.TablePodcastTag)
                .Columns(DatabaseSchema.ColumnPodcastId, DatabaseSchema.ColumnTagId);
        }

        public override void Down()
        {
            Delete.Table(DatabaseSchema.TablePodcastTag);
            Delete.Table(DatabaseSchema.TableTag);
        }

    }
}