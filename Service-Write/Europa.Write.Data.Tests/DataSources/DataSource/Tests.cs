using System;
using Europa.Infrastructure;
using Europa.Write.Data.DataSources;
using Npgsql;
using Xunit;

namespace Europa.Write.Data.Tests.DataSources.DataSource
{
    [Collection(Helper.IntegrationTests)] // Not in Parallel
    public class Tests : IDisposable
    {
        private readonly Helper _helper;
        private readonly IDataSource<Tag> _sut;

        public Tests()
        {
            var config = new Configuration();

            _helper = new Helper(config);
            _helper.Setup("");

            // sut
            _sut = new DataSource<Tag>(new NpgsqlConnection(config.ConnectionString));
        }

        public void Dispose()
        {
            _helper.Dispose();
        }
        

        [Fact]
        public void Creating_a_record_assigns_an_id()
        {
            var item = new Tag { Name = "test" };
            _sut.Save(item);
            Assert.NotEqual(item.Id, Guid.Empty);
        }

        [Fact]
        public void Creating_a_record_with_an_id_uses_the_supplied_id()
        {
            var id = Guid.NewGuid();
            var item = new Tag { Id = id, Name = "test" };
            _sut.Save(item);
            Assert.Equal(item.Id, id);
        }

        [Fact]
        public void Creating_a_record_can_be_found_to_exist()
        {
            var item = new Tag { Name = "test" };
            _sut.Save(item);
            Assert.True(_sut.Exists(item.Id));
        }

        [Fact]
        public void Updating_a_record()
        {
            var nameOriginal = "original";
            var nameUpdated = "updated";

            var item= new Tag { Name = nameOriginal };
            _sut.Save(item);

            item.Name = nameUpdated;
            _sut.Update(item);

            var itemUpdated = _sut.Get(item.Id);
            Assert.Equal(itemUpdated.Name, nameUpdated);
        }

    }
}
