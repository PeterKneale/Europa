using System.Threading.Tasks;
using Europa.Infrastructure;
using Europa.Write.Data;
using Europa.Write.Messages;

namespace Europa.Write.Handlers
{
    public class CreateCategoryHandler : ICommandHandler<CreateCategoryCommand>
    {
        private readonly IUnitOfWorkFactory _factory;
        private readonly IEventDispatcher _eventDispatcher;

        public CreateCategoryHandler(IUnitOfWorkFactory factory, IEventDispatcher eventDispatcher)
        {
            _factory = factory;
            _eventDispatcher = eventDispatcher;
        }

        public async Task Execute(CreateCategoryCommand command)
        {
            var id = command.Id;
            var name = command.Name;

            using (var work = _factory.Begin())
            {
                if (work.Categories.Exists(id))
                {
                    return;
                }

                var category = new Category { Id = id, Name = name };
                work.Categories.Save(category);
                work.Commit();
            }

            await _eventDispatcher.Publish(new CategoryCreatedEvent { Id = id });
        }
    }
}