using System;
using Europa.Write.Messages;
using Xunit;

namespace Europa.Write.Handlers.Tests
{
    public class CreateCategoryHandlerTests : IDisposable
    {
        private readonly Helper _helper;
        private readonly CreateCategoryHandler _handler;

        public CreateCategoryHandlerTests()
        {
            _helper = new Helper();
            _handler = new CreateCategoryHandler(_helper.MockUnitOfWorkFactory.Object, _helper.MockEventDispatcher.Object);
        }

        public void Dispose()
        {
            _helper.Verify();
        }

        [Fact]
        public void Create_category_when_id_does_not_exist()
        {
            var id = Guid.NewGuid();
            var name = "category 1";

            _helper
                .SetupFactoryToBeginWork()
                    .SetupCategoriesToExist(id, false)
                    .SetupCategoriesToCreate(id, name)
                .SetupWorkToCommit()
                .SetupWorkToDispose()
                .SetupCategorySavedEvent(new CategoryCreatedEvent { Id = id });

            //act
            _handler.Execute(new CreateCategoryCommand { Id = id, Name = name });
        }

        [Fact]
        public void Create_category_when_id_exists()
        {
            var id = Guid.NewGuid();
            var name = "category 1";

            _helper
                .SetupFactoryToBeginWork()
                .SetupCategoriesToExist(id, true)
                .SetupWorkToDispose();

            //act
            _handler.Execute(new CreateCategoryCommand { Id = id, Name = name });
        }
    }
}

