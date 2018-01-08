using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Europa.Query.Data;
using Europa.Query.Data.DataSources;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;
using FluentAssertions;
using Moq;
using Xunit;
using AutoMapper;

namespace Europa.Query.Handlers.Tests
{
    public class GetCategoriesTests : IDisposable
    {
        private class TestHelper
        {
            private readonly MockRepository _repo;
            private readonly Mock<ICategoryDataSource> _mockCategories;
            
            public TestHelper()
            {
                _repo = new MockRepository(MockBehavior.Strict);
                _mockCategories = _repo.Create<ICategoryDataSource>();

                // Setup Automapper
                Mapper.Initialize(x =>
                {
                    x.AddProfile<Mappings>();
                });
                Mapper.AssertConfigurationIsValid();
            }

            public TestHelper SetupListToReturn(IEnumerable<CategoryData> categories)
            {
                _mockCategories
                    .Setup(x => x.List())
                    .Returns(categories)
                    .Verifiable();

                return this;
            }
            
            public GetCategoriesQueryHandler CreateSut()
            {
                return new GetCategoriesQueryHandler(_mockCategories.Object);
            }

            public void Verify()
            {
                _repo.VerifyAll();
            }

        }

        private readonly TestHelper _testHelper;

        public GetCategoriesTests()
        {
            _testHelper = new TestHelper();
        }

        public void Dispose()
        {
            _testHelper.Verify();
        }

        [Fact]
        public async Task List_categories_with_empty_list()
        {
            // arrange
            _testHelper
                .SetupListToReturn(Enumerable.Empty<CategoryData>());

            //act
            var sut = _testHelper.CreateSut();
            var result = await sut.Execute(new GetCategoriesQuery());

            // assert
            result.Categories.Should().BeEmpty();
        }

        [Fact]
        public async Task List_categories()
        {
            // arrange
            var data = new List<CategoryData> {
                new CategoryData { Id = Guid.NewGuid(), Name = "test1" },
                new CategoryData { Id = Guid.NewGuid(), Name = "test2" }
            };
            var expected = new List<Category> {
                new Category { Id = data[0].Id, Name = data[0].Name },
                new Category { Id = data[1].Id, Name = data[1].Name }
            };

            _testHelper
                .SetupListToReturn(data);

            //act
            var sut = _testHelper.CreateSut();
            var result = await sut.Execute(new GetCategoriesQuery());

            // assert
            result.Categories.ShouldBeEquivalentTo(expected);
        }
    }
}

