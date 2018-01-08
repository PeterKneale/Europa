namespace Europa.Database
{
    public class DatabaseSchema
    {
        public const string TablePodcast = "podcast";
        public const string TableEpisode = "episode";
        public const string TableCategory = "category";
        public const string TableTag = "tag";
        public const string TablePodcastTag = "podcast_tag";

        public const string ViewPodcast = "podcast_view";
        public const string ViewCategory = "category_view";

        public const string ColumnId = "id";
        public const string ColumnPodcastId = "podcast_id";
        public const string ColumnTagId = "tag_id";
        public const string ColumnName = "name";
        public const string ColumnLink = "link";
        public const string ColumnTitle = "title";
        public const string ColumnDescription = "description";
        public const string ColumnCategoryId = "category_id";
        public const string ColumnCategoryName = "category_name";
        public const string ColumnTags = "tags";
        public const string ColumnPodcastCount = "podcast_count";

        public const string ConstrainUniquePodcastTag = "unique_podcast_tag";

        public const int ColumnNameLength = 50;
        public const int ColumnTitleLength = 50;
        public const int ColumnLinkLength = 400;
        public const int ColumnDescriptionLength = 400;
        
    }
}