using System;
using Microsoft.AspNetCore.Mvc;
using Europa.Web.Models;
using AutoMapper;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Query.Messages;
using Europa.Query.Messages.Models;

namespace Europa.Web.Controllers
{
    public class PodcastController : Controller
    {
        private readonly IQueryDispatcher _queries;
        
        public PodcastController(IQueryDispatcher queries)
        {
            _queries = queries;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var podcastResult = await _queries.Request<GetPodcastQuery, GetPodcastQueryResult>(new GetPodcastQuery { Id = id });
            var categoryResult = await _queries.Request<GetCategoryQuery, GetCategoryQueryResult>(new GetCategoryQuery { Id = podcastResult.Podcast.Id });
            var model = new PodcastPageModel
            {
                Category = Mapper.Map<Category, CategoryViewModel>(categoryResult.Category),
                Podcast = Mapper.Map<Podcast, PodcastViewModel>(podcastResult.Podcast)
            };
            return View(model);
        }
    }
}
