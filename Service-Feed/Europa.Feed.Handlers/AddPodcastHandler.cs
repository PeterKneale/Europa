using Europa.Infrastructure;
using Europa.Feed.Messages;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using Europa.Feed.Messages.Models;
using System.Linq;

namespace Europa.Feed.Handlers
{
    public class AddPodcastHandler : ICommandHandler<RetrievePodcastCommand>
    {
        private readonly ICommandDispatcher _commands;
        private readonly IEventDispatcher _events;

        public AddPodcastHandler(ICommandDispatcher commands, IEventDispatcher events)
        {
            _commands = commands;
            _events = events;
        }

        public async Task Execute(RetrievePodcastCommand command)
        {
            var feed = await FeedReader.ReadAsync(command.Link);

            var podcast = new Podcast
            {
                Title = feed.Title,
                Description = feed.Description,
                Link = feed.Link,
                Episodes = feed.Items.Select(x => new PodcastEpisode
                {
                    Title = x.Title,
                    Description = x.Description,
                    Author = x.Author,
                    Content = x.Content,
                    Link = x.Link
                }).ToArray()
            };

            await _events.Publish(new PodcastRetrievedEvent { Id = command.Id, Podcast = podcast });
        }
    }
}
