using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Write.Data;
using Europa.Write.Messages;

namespace Europa.Write.Handlers
{
    public class DeletePodcastHandler : ICommandHandler<DeletePodcastCommand>
    {
        private readonly IUnitOfWorkFactory _factory;
        private readonly IEventDispatcher _eventDispatcher;

        public DeletePodcastHandler(IUnitOfWorkFactory factory, IEventDispatcher eventDispatcher)
        {
            _factory = factory;
            _eventDispatcher = eventDispatcher;
        }

        public async Task Execute(DeletePodcastCommand command)
        {
            var id = command.Id;
            using (var work = _factory.Begin())
            {
                work.Podcasts.Delete(command.Id);
                work.Commit();
            }

            await _eventDispatcher.Publish(new PodcastDeletedEvent { Id = id });
        }
    }
}