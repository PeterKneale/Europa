using System;
using Europa.Infrastructure;
using Europa.Write.Data.DataSources;
using Npgsql;
using Xunit;

namespace Europa.Write.Data.Tests.DataSources.PodcastTags
{
    [Collection(Helper.IntegrationTests)] // Not in Parallel
    public class Tests : IDisposable
    {
        private readonly Helper _helper;
        private readonly IPodcastTagDataSource _sut;

        public Tests()
        {
            var config = new Configuration();

            _helper = new Helper(config);
            _helper.Setup(TestData.FileName);

            // sut
            _sut = new PodcastTagDataSource(new NpgsqlConnection(config.ConnectionString));
        }

        public void Dispose()
        {
            _helper.Dispose();
        }
        
        [Fact]
        public void Check_initial_state_by_tag_ids()
        {
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast1, TestData.Tag1Id));
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast1, TestData.Tag2Id));
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast1, TestData.Tag3Id));
            Assert.True(_sut.IsPodcastTagged(TestData.Podcast2, TestData.Tag1Id));
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast2, TestData.Tag2Id));
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast2, TestData.Tag3Id));
            Assert.True(_sut.IsPodcastTagged(TestData.Podcast3, TestData.Tag1Id));
            Assert.True(_sut.IsPodcastTagged(TestData.Podcast3, TestData.Tag2Id));
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast3, TestData.Tag3Id));
        }

        [Fact]
        public void Check_initial_state_by_tag_names()
        {
            // Podcast 1 in initial state
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast1, TestData.Tag1));
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast1, TestData.Tag2));
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast1, TestData.Tag3));
            // Podcast 2 in initial state
            Assert.True(_sut.IsPodcastTagged(TestData.Podcast2, TestData.Tag1));
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast2, TestData.Tag2));
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast2, TestData.Tag3));
            // Podcast 3 in initial state
            Assert.True(_sut.IsPodcastTagged(TestData.Podcast3, TestData.Tag1));
            Assert.True(_sut.IsPodcastTagged(TestData.Podcast3, TestData.Tag2));
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast3, TestData.Tag3));
        }
        
        [Fact]
        public void Tag_Podcast_with_tag_id_that_exists()
        {
            _sut.TagPodcast(TestData.Podcast1, TestData.Tag1Id);
            Assert.True(_sut.IsPodcastTagged(TestData.Podcast1, TestData.Tag1Id));
        }

        [Fact]
        public void Tag_Podcast_with_tag_that_exists()
        {
            _sut.TagPodcast(TestData.Podcast1, TestData.Tag1);
            Assert.True(_sut.IsPodcastTagged(TestData.Podcast1, TestData.Tag1));
        }

        [Fact]
        public void Tag_Podcast_with_tag_id_that_does_not_exist()
        {
            // TODO: This behaves differently from the below test
            var ex = Record.Exception(() => _sut.TagPodcast(TestData.Podcast1, TestData.TagDoesNotExistId));
            Assert.Contains("violates foreign key constraint ", ex.Message);
        }

        [Fact]
        public void Tag_Podcast_with_tag_name_that_does_not_exist()
        {
            var tag = "new-tag-name";
            var ex = Record.Exception(() => _sut.TagPodcast(TestData.Podcast1, tag));
            Assert.Contains("tag does not exist", ex.Message);
        }

        [Fact]
        public void Untag_Podcast_by_id_when_it_has_a_tag()
        {
            _sut.UnTagPodcast(TestData.Podcast2, TestData.Tag1Id);
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast2, TestData.Tag1Id));
        }

        [Fact]
        public void Untag_Podcast_by_name_when_it_has_a_tag()
        {
            _sut.UnTagPodcast(TestData.Podcast2, TestData.Tag1);
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast2, TestData.Tag1));
        }

        [Fact]
        public void Untag_Podcast_by_name_when_it_has_no_tag()
        {
            _sut.UnTagPodcast(TestData.Podcast2, TestData.TagDoesNotExist);
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast2, TestData.TagDoesNotExist));
        }

        [Fact]
        public void Untag_Podcast_by_id_when_it_has_no_tag()
        {
            _sut.UnTagPodcast(TestData.Podcast2, TestData.TagDoesNotExistId);
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast2, TestData.TagDoesNotExistId));
        }

        [Fact]
        public void Untag_Podcast_with_tag_that_exists()
        {
            _sut.UnTagPodcast(TestData.Podcast2, TestData.Tag1Id);
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast1, TestData.Tag1Id));
        }

        [Fact]
        public void Untag_Podcast_with_tag_that_exists_but_not_applied()
        {
            _sut.UnTagPodcast(TestData.Podcast1, TestData.Tag1Id);
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast1, TestData.Tag1Id));
        }

        [Fact]
        public void Untag_Podcast_with_tag_that_does_not_exist()
        {
            _sut.UnTagPodcast(TestData.Podcast1, TestData.TagDoesNotExistId);
            Assert.False(_sut.IsPodcastTagged(TestData.Podcast1, TestData.TagDoesNotExistId));
        }
    }
}
