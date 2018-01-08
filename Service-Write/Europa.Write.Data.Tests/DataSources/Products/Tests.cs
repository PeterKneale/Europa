using System;
using System.Linq;
using Europa.Infrastructure;
using Europa.Write.Data.DataSources;
using Npgsql;
using Xunit;

namespace Europa.Write.Data.Tests.DataSources.Podcasts
{
    [Collection(Helper.IntegrationTests)] // Not in Parallel
    public class Tests : IDisposable
    {
        private readonly Helper _helper;
        private readonly IPodcastDataSource _sut;

        public Tests()
        {
            var config = new Configuration();

            _helper = new Helper(config);
            _helper.Setup(TestData.FileName);

            // sut
            _sut = new PodcastDataSource(new NpgsqlConnection(config.ConnectionString));
        }

        public void Dispose()
        {
            _helper.Dispose();
        }

        [Fact]
        public void Creating_a_podcast_assigns_an_id()
        {
            var podcast = new Podcast { Title = "test", CategoryId = TestData.Category1 };
            _sut.Save(podcast);
            Assert.NotEqual(Guid.Empty, podcast.Id);
        }

        [Fact]
        public void Creating_a_podcast_without_specifying_a_category_throws_an_exception()
        {
            var podcast = new Podcast { Title = "test" };
            var ex = Record.Exception(() => _sut.Save(podcast));
            Assert.Contains("foreign", ex.Message);
        }

        [Fact]
        public void Creating_a_podcast_specifying_a_nonexistant_category_throws_an_exception()
        {
            var ex = Record.Exception(() => _sut.Save(new Podcast { Title = "test", CategoryId = TestData.CategoryDoesNotExist }));
            Assert.Contains("foreign", ex.Message);
        }

        [Fact]
        public void Creating_a_podcast_specifying_a_null_name_throws_an_exception()
        {
            var ex = Record.Exception(() => _sut.Save(new Podcast { Title = null, CategoryId = TestData.CategoryDoesNotExist }));
            Assert.Contains("not-null constraint", ex.Message);
        }

        [Fact]
        public void List_podcasts_should_return_list()
        {
            Assert.NotEmpty(_sut.List());
        }

        [Fact]
        public void List_podcasts_in_category()
        {
            Check_podcasts_in_category(TestData.Category1, new[] { TestData.Podcast1Category1, TestData.Podcast2Category1 });
            Check_podcasts_in_category(TestData.Category2, new[] { TestData.Podcast1Category2, TestData.Podcast2Category2 });
            Check_podcasts_in_category(TestData.Category3, new Guid[] { });
        }

        private void Check_podcasts_in_category(Guid category, Guid[] expectedPodcasts)
        {
            var actual = _sut.ListByCategory(category).ToList();

            Assert.True(actual.Select(x => x.Id).SequenceEqual(expectedPodcasts));
            Assert.True(actual.All(x => x.CategoryId == category));
        }
        
        [Fact]
        public void Deleting_a_podcast_works()
        {
            _sut.Delete(TestData.Podcast1Category1);
            Assert.Null(_sut.Get(TestData.Podcast1Category1));
        }
    }
}
