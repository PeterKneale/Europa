using Europa.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Messages;

namespace Europa.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WaitController : Controller
    {
        private readonly ICommandDispatcher _commands;
        private readonly IQueryDispatcher _queries;

        public WaitController(ICommandDispatcher commands, IQueryDispatcher queries)
        {
            _commands = commands;
            _queries = queries;
        }

        public async Task<IActionResult> ForPodcastCreation(Guid id)
        {
            var result = await _queries.Request<GetPodcastQuery, GetPodcastQueryResult>(new GetPodcastQuery { Id = id });
            if (result.Podcast == null)
            {
                return View(new WaitModel { Id = id });
            }
            return RedirectToAction("Index", "Podcast", new { id });
        }

        public async Task<IActionResult> ForCategoryCreation(Guid id)
        {
            var result = await _queries.Request<GetCategoryQuery, GetCategoryQueryResult>(new GetCategoryQuery { Id = id });
            if (result.Category == null)
            {
                return View(new WaitModel { Id = id });
            }
            return RedirectToAction("Index", "Category", new { id });
        }
    }
}
