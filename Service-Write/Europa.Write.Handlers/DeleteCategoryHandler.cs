using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Write.Data;
using Europa.Write.Messages;

namespace Europa.Write.Handlers
{
    public class DeleteCategoryHandler : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWorkFactory _factory;
        private readonly IEventDispatcher _eventDispatcher;

        public DeleteCategoryHandler(IUnitOfWorkFactory factory, IEventDispatcher eventDispatcher)
        {
            _factory = factory;
            _eventDispatcher = eventDispatcher;
        }

        public async Task Execute(DeleteCategoryCommand command)
        {
            var id = command.Id;
            using (var work = _factory.Begin())
            {
                work.Categories.Delete(id);
                work.Commit();
            }

            await _eventDispatcher.Publish(new CategoryDeletedEvent { Id = id });
        }
    }
}