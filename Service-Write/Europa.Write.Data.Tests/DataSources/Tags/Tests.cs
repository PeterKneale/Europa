using System;
using System.Collections.Generic;
using System.Linq;
using Europa.Infrastructure;
using Europa.Write.Data.DataSources;
using Npgsql;
using Xunit;

namespace Europa.Write.Data.Tests.DataSources.Tags
{
    [Collection(Helper.IntegrationTests)] // Not in Parallel
    public class Tests : IDisposable
    {
        private readonly Helper _helper;
        private readonly ITagDataSource _sut;

        public Tests()
        {
            var config = new Configuration();

            _helper = new Helper(config);
            _helper.Setup(TestData.FileName);

            // sut
            _sut = new TagDataSource(new NpgsqlConnection(config.ConnectionString));
        }

        public void Dispose()
        {
            _helper.Dispose();
        }

        [Fact]
        public void List_should_return_list()
        {
            var list = _sut.List();
            Assert.True(list.SequenceEqual(new List<Tag> {
                new Tag{Id = TestData.TagId1, Name = TestData.TagName1},
                new Tag{Id = TestData.TagId2, Name = TestData.TagName2},
                new Tag{Id = TestData.TagId3, Name = TestData.TagName3}
            }, new TagComparer()));
        }

        [Fact]
        public void Creating_an_tag_with_the_same_name_fails()
        {
            var item = new Tag { Name = TestData.TagName1 };
            var ex = Record.Exception(() => _sut.Save(item));
            Assert.Contains("duplicate key value violates", ex.Message);
        }

        [Fact]
        public void Exists_returns_true_when_tag_exists()
        {
            Assert.True(_sut.Exists(TestData.TagName1));
        }

        [Fact]
        public void Exists_returns_false_when_tag_does_not_exist()
        {
            Assert.False(_sut.Exists("does_not_exist"));
        }

        [Fact]
        public void Get_returns_tag_when_tag_exists()
        {
            var tag = _sut.Get(TestData.TagName1);
            Assert.NotNull(tag);
            Assert.Equal(tag.Id, TestData.TagId1);
            Assert.Equal(tag.Name, TestData.TagName1);
        }

        [Fact]
        public void Get_returns_null_when_tag_does_not_exist()
        {
            Assert.Null(_sut.Get("does_not_exist"));
        }

        [Fact]
        public void Ensure_returns_tag_when_tag_does_not_exist()
        {
            var name = "does_not_exist";
            var tag = _sut.Ensure(name);
            Assert.NotNull(tag);
            Assert.Equal(tag.Name, name);
        }

        [Fact]
        public void Ensure_returns_tag_when_tag_does_exist()
        {
            var tag = _sut.Ensure(TestData.TagName1);
            Assert.NotNull(tag);
            Assert.Equal(tag.Id, TestData.TagId1);
            Assert.Equal(tag.Name, TestData.TagName1);
        }
    }
}
