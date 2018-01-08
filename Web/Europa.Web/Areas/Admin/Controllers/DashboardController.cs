using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Europa.Infrastructure;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;
using Europa.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Europa.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IQueryDispatcher _queries;

        public DashboardController(IQueryDispatcher queries)
        {
            _queries = queries;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _queries.Request<GetCategoriesQuery, GetCategoriesQueryResult>(new GetCategoriesQuery());
            var model = new DashboardPageModel
            {
                Categories = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(result.Categories)
            };
            return View(model);
        }
    }
}