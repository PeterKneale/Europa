using AutoMapper;
using Europa.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;
using Europa.Write.Messages;

namespace Europa.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICommandDispatcher _commands;
        private readonly IQueryDispatcher _queries;

        public CategoryController(ICommandDispatcher commands, IQueryDispatcher queries)
        {
            _commands = commands;
            _queries = queries;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var categoryResult = await _queries.Request<GetCategoryQuery, GetCategoryQueryResult>(new GetCategoryQuery { Id = id });
            var podcastsResult = await _queries.Request<GetPodcastsQuery, GetPodcastsQueryResult>(new GetPodcastsQuery { CategoryId = id });
            var model = new CategoryPageModel
            {
                Category = Mapper.Map<Category, CategoryViewModel>(categoryResult.Category),
                Podcasts = Mapper.Map<IEnumerable<Podcast>, IEnumerable<PodcastViewModel>>(podcastsResult.Podcasts)
            };
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var query = new GetCategoryQuery { Id = id };
            var result = await _queries.Request<GetCategoryQuery, GetCategoryQueryResult>(query);
            var model = Mapper.Map<Category, CategoryEditModel>(result.Category);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _commands.Send(new CreateCategoryCommand { Id = model.Id, Name = model.Name });

            return RedirectToAction("Index", new { id = model.Id });
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = Guid.NewGuid();
            await _commands.Send(new CreateCategoryCommand { Id = id, Name = model.Name });

            return RedirectToAction("ForCategoryCreation", "Wait", new { Id = id });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            await _commands.Send(command);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
