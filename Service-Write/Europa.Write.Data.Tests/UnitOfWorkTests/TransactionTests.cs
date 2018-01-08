using System;
using Europa.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Europa.Write.Data.Tests.UnitOfWorkTests
{
    [Collection(Helper.IntegrationTests)] // Not in Parallel
    public class TransactionTests : IDisposable
    {
        private readonly Helper _helper;
        private readonly IUnitOfWork _work;

        public TransactionTests()
        {
            var config = new Configuration();

            _helper = new Helper(config);
            _helper.Setup("");

            // sut
            _work = new UnitOfWork(config, new Mock<ILogger<UnitOfWork>>().Object);
        }

        public void Dispose()
        {
            _work.Dispose();
            _helper.Dispose();
        }
        
        [Fact]
        public void Creating_two_records_in_a_transaction_can_be_read()
        {
            _work.Begin();
            var category = new Category { Name = "test" };
            _work.Categories.Save(category);
            var podcast1 = new Podcast { Title = "test 1", CategoryId = category.Id };
            var podcast2 = new Podcast { Title = "test 2", CategoryId = category.Id };
            _work.Podcasts.Save(podcast1);
            _work.Podcasts.Save(podcast2);
            _work.Commit();
            Assert.NotNull(_work.Categories.Get(category.Id));
            Assert.NotNull(_work.Podcasts.Get(podcast1.Id));
            Assert.NotNull(_work.Podcasts.Get(podcast2.Id));
        }

        [Fact]
        public void Creating_two_records_in_a_transaction__that_is_rolled_back_can_not_be_read()
        {
            _work.Begin();
            var category = new Category { Name = "test" };
            _work.Categories.Save(category);
            var podcast1 = new Podcast { Title = "test 1", CategoryId = category.Id };
            var podcast2 = new Podcast { Title = "test 2", CategoryId = category.Id };
            _work.Podcasts.Save(podcast1);
            _work.Podcasts.Save(podcast2);
            _work.Rollback();
            Assert.Null(_work.Podcasts.Get(podcast1.Id));
            Assert.Null(_work.Podcasts.Get(podcast2.Id));
        }

        [Fact]
        public void Deleting_a_record_then_rolling_back_can_be_read()
        {
            // Create
            _work.Begin();
            var category = new Category { Name = "test" };
            _work.Categories.Save(category);
            _work.Commit();

            // Assert
            var id = category.Id;
            Assert.NotNull(_work.Categories.Get(id));

            // Delete but rollback
            _work.Begin();
            _work.Categories.Delete(id);
            _work.Rollback();

            // Assert
            Assert.NotNull(_work.Categories.Get(id));
        }

    }
}
