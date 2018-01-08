using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Write.Data;
using Europa.Write.Messages;
using Microsoft.Extensions.Logging;

namespace Europa.Write.Handlers
{
    public class PopulateHandler : ICommandHandler<PopulateCommand>
    {
        private readonly IUnitOfWorkFactory _factory;
        private readonly ILogger<PopulateHandler> _logger;
        private readonly IEventDispatcher _eventDispatcher;

        public PopulateHandler(IUnitOfWorkFactory factory, ILogger<PopulateHandler> logger, IEventDispatcher eventDispatcher)
        {
            _factory = factory;
            _logger = logger;
            _eventDispatcher = eventDispatcher;
        }

        public async Task Execute(PopulateCommand command)
        {
            var categories = new List<CategoryData>
            {
                new CategoryData
                {
                    Name = "Laptop",
                    Podcasts = new List<PodcastData>
                    {
                        new PodcastData{Name = "Dell XPS", Tags = new[]{"fast", "good"}},
                        new PodcastData{Name = "Dell Inspiron", Tags = new[]{"fast", "cheap"}}
                    }
                },
                new CategoryData{
                    Name = "Desktop",
                    Podcasts = new List<PodcastData>
                    {
                        new PodcastData{Name = "Dell WorkStation", Tags = new[]{"good", "cheap"}},
                        new PodcastData{Name = "Dell Slimline Desktop", Tags = new[]{"fast", "cheap"}}
                    },
                },
                new CategoryData{
                    Name = "Server",
                    Podcasts = new List<PodcastData>
                    {
                        new PodcastData{Name = "Dell Server", Tags = new[]{"good", "cheap"}},
                        new PodcastData{Name = "IBM Server", Tags = new[]{"fast", "good"}}
                    }
                }
            };
            var ids = new List<Guid>();
            using (var work = _factory.Begin())
            {
                var tags = categories.SelectMany(c => c.Podcasts.SelectMany(p => p.Tags)).Distinct();
                foreach (var tag in tags)
                {
                    work.Tags.Ensure(tag);
                }

                foreach (var category in categories)
                {
                    var c = new Category { Name = $"{category.Name}" };
                    _logger.LogInformation($"Creating category {c.Name}");
                    work.Categories.Save(c);

                    foreach (var podcast in category.Podcasts)
                    {
                        var p = new Podcast { Title = $"{podcast.Name}", CategoryId = c.Id };
                        _logger.LogInformation($"Creating podcast {p.Title}");
                        work.Podcasts.Save(p);
                        foreach (var tag in podcast.Tags)
                        {
                            _logger.LogInformation($"Creating tag {tag}");
                            work.PodcastTags.TagPodcast(p.Id, tag);
                        }
                        ids.Add(p.Id);
                    }
                }

                work.Commit();
                foreach (var id in ids)
                {
                    await _eventDispatcher.Publish(new PodcastCreatedEvent { Id = id });
                }
            }
        }

        class CategoryData
        {
            public string Name;
            public IEnumerable<PodcastData> Podcasts;
        }

        class PodcastData
        {
            public string Name;
            public string[] Tags;
        }
    }

}