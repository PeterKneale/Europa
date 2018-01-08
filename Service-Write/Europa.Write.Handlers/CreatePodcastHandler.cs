using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Write.Data;
using Europa.Write.Messages;
using Europa.Feed.Messages;

namespace Europa.Write.Handlers
{
    public class CreatePodcastHandler : ICommandHandler<CreatePodcastCommand>
    {
        private readonly IUnitOfWorkFactory _factory;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ICommandDispatcher _command;

        public CreatePodcastHandler(IUnitOfWorkFactory factory, IEventDispatcher eventDispatcher, ICommandDispatcher command)
        {
            _factory = factory;
            _eventDispatcher = eventDispatcher;
            _command = command;
        }

        public async Task Execute(CreatePodcastCommand command)
        {
            using (var work = _factory.Begin())
            {
                if (work.Podcasts.Exists(command.Id))
                {
                    return;
                }

                var Podcast = new Podcast {
                    Id = command.Id,
                    Link = command.Link,
                    CategoryId = command.CategoryId
                };

                work.Podcasts.Save(Podcast);
                work.Commit();
            }
            
            await _command.Send(new RetrievePodcastCommand { Id = command.Id, Link = command.Link });
        }
    }
}