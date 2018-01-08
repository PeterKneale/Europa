using AutoMapper;
using Europa.Web.Controllers;
using Europa.Web.Models;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;
using Xunit;

namespace Europa.Web.Tests
{
    public class HomeControllerTests
    {
        public HomeControllerTests()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(Mappings)));
        }

        [Fact]
        public async Task Index_Action_Returns_ViewAsync()
        {
            // Arrange
            var repo = new MockRepository(MockBehavior.Strict);
            var dispatcherMock = repo.Create<IQueryDispatcher>();
            dispatcherMock
                .Setup(x => x.Request<GetCategoriesQuery, GetCategoriesQueryResult>(It.IsAny<GetCategoriesQuery>()))
                .Returns(Task.FromResult(new GetCategoriesQueryResult { Categories = Enumerable.Empty<Category>() }))
                .Verifiable();

            // Act
            var controller = new HomeController(dispatcherMock.Object);
            var result = await controller.Index();

            // Assert
            dispatcherMock.VerifyAll();

            result.Should().BeViewResult();
            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();
            var model = viewResult.Model as HomePageModel;
            model.Should().NotBeNull();
        }
    }
}
