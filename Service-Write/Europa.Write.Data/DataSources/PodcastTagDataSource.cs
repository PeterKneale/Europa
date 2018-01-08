using System;
using System.Data;
using Dapper;
using DB = Europa.Database.DatabaseSchema;

namespace Europa.Write.Data.DataSources
{
    public interface IPodcastTagDataSource : IDataSource<PodcastTag>
    {
        bool IsPodcastTagged(Guid PodcastId, Guid tagId);
        void TagPodcast(Guid PodcastId, Guid tagId);
        void UnTagPodcast(Guid PodcastId, Guid tagId);
        bool IsPodcastTagged(Guid PodcastId, string tag);
        void TagPodcast(Guid PodcastId, string tag);
        void UnTagPodcast(Guid PodcastId, string tag);
    }

    public class PodcastTagDataSource : DataSource<PodcastTag>, IPodcastTagDataSource
    {
        public PodcastTagDataSource(IDbConnection connection, IDbTransaction transaction = null) : base(connection, transaction)
        {
        }

        public void TagPodcast(Guid PodcastId, Guid tagId)
        {
            var PodcastTag = new PodcastTag { PodcastId = PodcastId, TagId = tagId };
            Save(PodcastTag);
        }

        public void TagPodcast(Guid PodcastId, string tagName)
        {
            var tag = GetTagByName(tagName);
            if (tag == null)
                throw new Exception("tag does not exist");
            var PodcastTag = new PodcastTag { PodcastId = PodcastId, TagId = tag.Id };
            Save(PodcastTag);
        }

        public void UnTagPodcast(Guid PodcastId, Guid tagId)
        {
            var PodcastTag = GetPodcastTag(PodcastId, tagId);
            if (PodcastTag != null)
            {
                Delete(PodcastTag.Id);
            }
        }

        public void UnTagPodcast(Guid PodcastId, string tagName)
        {
            var tag = GetTagByName(tagName);
            if (tag == null)
            {
                // If the tag doesn't exist, the Podcast is already effectively untagged
                return;
            }
            var PodcastTag = GetPodcastTag(PodcastId, tag.Id);
            if (PodcastTag != null)
            {
                Delete(PodcastTag.Id);
            }
        }

        public bool IsPodcastTagged(Guid PodcastId, Guid tagId)
        {
            return GetPodcastTag(PodcastId, tagId) != null;
        }

        public bool IsPodcastTagged(Guid PodcastId, string tagName)
        {
            var tag = GetTagByName(tagName);
            return tag != null && IsPodcastTagged(PodcastId, tag.Id);
        }

        private PodcastTag GetPodcastTag(Guid PodcastId, Guid tagId)
        {
            var query = $"select * from {DB.TablePodcastTag} where {DB.ColumnPodcastId} = @PodcastId and {DB.ColumnTagId} = @tagId";
            return Connection.QueryFirstOrDefault<PodcastTag>(query, new { PodcastId, tagId });
        }
        
        private Tag GetTagByName(string name)
        {
            var query = $"select * from {DB.TableTag} where {DB.ColumnName} = @name";
            return Connection.QueryFirstOrDefault<Tag>(query, new { name });
        }
    }
}
