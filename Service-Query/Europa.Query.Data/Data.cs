using System;
using System.Collections.Generic;
using Dapper;
using Europa.Database;

namespace Europa.Query.Data
{
    public interface IData
    {
        Guid Id { get; set; }
    }

    public class Data : IData
    {
        [Key]
        [Column(DatabaseSchema.ColumnId)]
        public Guid Id { get; set; }
    }

    [Table(DatabaseSchema.ViewPodcast)]
    public class PodcastData : Data
    {
        [Column(DatabaseSchema.ColumnCategoryId)]
        public Guid CategoryId { get; set; }

        [Column(DatabaseSchema.ColumnCategoryName)]
        public string CategoryName { get; set; }

        [Column(DatabaseSchema.ColumnTitle)]
        public string Title { get; set; }

        [Column(DatabaseSchema.ColumnTags)]
        public string Tags { get; set; }
    }

    [Table(DatabaseSchema.ViewCategory)]
    public class CategoryData : Data
    {
        [Column(DatabaseSchema.ColumnName)]
        public string Name { get; set; }

        [Column(DatabaseSchema.ColumnPodcastCount)]
        public int PodcastCount { get; set; }
    }

}
