using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Europa.Web.Models;
using AutoMapper;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Messages;
using Europa.Search.Messages;

namespace Europa.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueryDispatcher _queries;

        public HomeController(IQueryDispatcher queries)
        {
            _queries = queries;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _queries.Request<SearchQuery, SearchQueryResult>(new SearchQuery { Query="car" });
            return View();
        }

        public IActionResult About()
        {

            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
