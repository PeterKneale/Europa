using System.Data;
using FluentMigrator;

namespace Europa.Database.Migrations
{
    [Migration(1, "Add Categories and Podcasts")]
    public class Migration1 : Migration
    {
        public override void Up()
        {
            Create.Table(DatabaseSchema.TableCategory)
                .WithColumn(DatabaseSchema.ColumnId)
                    .AsGuid()
                    .PrimaryKey()
                    .NotNullable()
                .WithColumn(DatabaseSchema.ColumnName)
                    .AsString(DatabaseSchema.ColumnNameLength)
                    .Unique()
                    .NotNullable();

            Create.Table(DatabaseSchema.TablePodcast)
                .WithColumn(DatabaseSchema.ColumnId)
                    .AsGuid()
                    .PrimaryKey()
                    .NotNullable()
                .WithColumn(DatabaseSchema.ColumnTitle)
                    .AsString(DatabaseSchema.ColumnTitleLength)
                    .Nullable()
                .WithColumn(DatabaseSchema.ColumnDescription)
                    .AsString(DatabaseSchema.ColumnDescriptionLength)
                    .Nullable()
                .WithColumn(DatabaseSchema.ColumnLink)
                    .AsString(DatabaseSchema.ColumnLinkLength)
               .WithColumn(DatabaseSchema.ColumnCategoryId)
                    .AsGuid()
                    .ForeignKey(DatabaseSchema.TableCategory, DatabaseSchema.ColumnId)
                    .OnDelete(Rule.Cascade);

            Create.Table(DatabaseSchema.TableEpisode)
                .WithColumn(DatabaseSchema.ColumnId)
                    .AsGuid()
                    .PrimaryKey()
                    .NotNullable()
                .WithColumn(DatabaseSchema.ColumnTitle)
                    .AsString(DatabaseSchema.ColumnTitleLength)
                .WithColumn(DatabaseSchema.ColumnDescription)
                    .AsString(DatabaseSchema.ColumnDescriptionLength)
                .WithColumn(DatabaseSchema.ColumnLink)
                    .AsString(DatabaseSchema.ColumnLinkLength)
               .WithColumn(DatabaseSchema.ColumnPodcastId)
                    .AsGuid()
                    .ForeignKey(DatabaseSchema.TablePodcast, DatabaseSchema.ColumnId)
                    .OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table(DatabaseSchema.TableEpisode);
            Delete.Table(DatabaseSchema.TablePodcast);
            Delete.Table(DatabaseSchema.TableCategory);
        }

    }
}