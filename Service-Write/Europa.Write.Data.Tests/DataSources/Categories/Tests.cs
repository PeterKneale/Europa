using System;
using Europa.Infrastructure;
using Europa.Write.Data.DataSources;
using Npgsql;
using Xunit;

namespace Europa.Write.Data.Tests.DataSources.Categories
{
    [Collection(Helper.IntegrationTests)] // Not in Parallel
    public class CategoryDataSourceTests : IDisposable
    {
        private readonly Helper _helper;
        private readonly ICategoryDataSource _sut;

        public CategoryDataSourceTests()
        {
            var config = new Configuration();

            _helper = new Helper(config);
            _helper.Setup(TestData.FileName);

            // sut
            _sut = new CategoryDataSource(new NpgsqlConnection(config.ConnectionString));
        }

        public void Dispose()
        {
            _helper.Dispose();
        }

        
        [Fact]
        public void List_categories_should_return_list()
        {
            Assert.NotEmpty(_sut.List());
        }
        
        [Fact]
        public void Creating_a_category_assigns_an_id()
        {
            var category = new Category { Name = "test" };
            _sut.Save(category);
            Assert.NotEqual(category.Id, Guid.Empty);
        }

        [Fact]
        public void Creating_a_categories_with_the_same_name_works()
        {
            var name = "test category";
            var category1 = new Category { Name = name };
            var category2 = new Category { Name = name };
            _sut.Save(category1);
            var ex = Record.Exception(() => _sut.Save(category2));
            Assert.Contains("duplicate key value violates", ex.Message);
        }
        
        [Fact]
        public void Deleting_a_category_with_attached_podcasts_should_succeed()
        {
            _sut.Delete(TestData.Category1);
        }
    }
}
