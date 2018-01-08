using System;
using Dapper;
using Europa.Database;

namespace Europa.Write.Data
{
    public interface IData
    {
        Guid Id { get; set; }
    }

    public class Data: IData
    {
        [Key]
        [Column(DatabaseSchema.ColumnId)]
        public Guid Id { get; set; }
    }

    [Table(DatabaseSchema.TablePodcast)]
    public class Podcast : Data
    {
        [Column(DatabaseSchema.ColumnTitle)]
        public string Title { get; set; }

        [Column(DatabaseSchema.ColumnDescription)]
        public string Description { get; set; }

        [Column(DatabaseSchema.ColumnLink)]
        public string Link { get; set; }

        [Column(DatabaseSchema.ColumnCategoryId)]
        public Guid CategoryId { get; set; }
    }

    [Table(DatabaseSchema.TableEpisode)]
    public class Episode : Data
    {
        [Column(DatabaseSchema.ColumnTitle)]
        public string Title { get; set; }

        [Column(DatabaseSchema.ColumnDescription)]
        public string Description { get; set; }

        [Column(DatabaseSchema.ColumnLink)]
        public string Link { get; set; }

        [Column(DatabaseSchema.ColumnPodcastId)]
        public Guid PodcastId { get; set; }
    }

    [Table(DatabaseSchema.TableCategory)]
    public class Category : Data
    {
        [Column(DatabaseSchema.ColumnName)]
        public string Name { get; set; }
    }

    [Table(DatabaseSchema.TableTag)]
    public class Tag : Data
    {
        [Column(DatabaseSchema.ColumnName)]
        public string Name { get; set; }
    }

    [Table(DatabaseSchema.TablePodcastTag)]
    public class PodcastTag : Data
    {
        [Column(DatabaseSchema.ColumnPodcastId)]
        public Guid PodcastId { get; set; }

        [Column(DatabaseSchema.ColumnTagId)]
        public Guid TagId { get; set; }
    }
}
