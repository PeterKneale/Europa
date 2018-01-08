using AutoMapper;
using Europa.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;
using Europa.Write.Messages;

namespace Europa.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PodcastController : Controller
    {
        private readonly ICommandDispatcher _commands;
        private readonly IQueryDispatcher _queries;

        public PodcastController(ICommandDispatcher commands, IQueryDispatcher queries)
        {
            _commands = commands;
            _queries = queries;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var result = await _queries.Request<GetPodcastQuery, GetPodcastQueryResult>(new GetPodcastQuery { Id = id });
            var model = Mapper.Map<Podcast, PodcastViewModel>(result.Podcast);
            return View(model);
        }

        public IActionResult Create(Guid id)
        {
            var model = new PodcastCreateModel { CategoryId = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PodcastCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = Guid.NewGuid();
            await _commands.Send(new CreatePodcastCommand { Id = id, Link = model.Link, CategoryId = model.CategoryId });
            return RedirectToAction("ForPodcastCreation", "Wait", new { Id = id });
        }
        
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePodcastCommand { Id = id };
            await _commands.Send(command);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
