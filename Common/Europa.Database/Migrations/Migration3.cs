using FluentMigrator;
using System;
using S = Europa.Database.DatabaseSchema;
namespace Europa.Database.Migrations
{
    [Migration(3, "Add Views")]
    public class Migration3 : Migration
    {
        public override void Up()
        {
            var category_view = $@"
                create view {S.ViewCategory} as 
                select 
                    c.{S.ColumnId}, 
                    c.{S.ColumnName}, 
                    (select count(1) from {S.TablePodcast} p where p.{S.ColumnCategoryId} = c.{S.ColumnId}) as {S.ColumnPodcastCount}
                from {S.TableCategory} c;";
            var product_view = $@"
                create view {S.ViewPodcast} as 
                select 
	                p.{S.ColumnId}, 
	                p.{S.ColumnTitle}, 
	                c.{S.ColumnId} as {S.ColumnCategoryId}, 
	                c.{S.ColumnName} as {S.ColumnCategoryName},
	                (
		                select string_agg({S.ColumnName}, ',') from {S.TableTag} t
		                join {S.TablePodcastTag} pt on t.{S.ColumnId} = pt.{S.ColumnTagId}
		                where pt.{S.ColumnPodcastId} = p.{S.ColumnId}
		                group by pt.{S.ColumnPodcastId}
	                ) as {S.ColumnTags}
                from {S.TablePodcast} p 
                join {S.TableCategory} c on p.{S.ColumnCategoryId} = c.{S.ColumnId};";

            Console.WriteLine(category_view);
            Console.WriteLine(product_view);
            Execute.Sql(category_view);
            Execute.Sql(product_view);
        }

        public override void Down()
        {
            Execute.Sql($"drop view {S.ViewCategory}");
            Execute.Sql($"drop view {S.ViewPodcast}");
        }
    }
}