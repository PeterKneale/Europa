using System;
using Europa.Write.Messages;
using Xunit;

namespace Europa.Write.Handlers.Tests
{
    public class DeleteCategoryHandlerTests : IDisposable
    {
        private readonly Helper _helper;
        private readonly DeleteCategoryHandler _handler;

        public DeleteCategoryHandlerTests()
        {
            _helper = new Helper();
            _handler = new DeleteCategoryHandler(_helper.MockUnitOfWorkFactory.Object, _helper.MockEventDispatcher.Object);
        }

        public void Dispose()
        {
            _helper.Verify();
        }

        [Fact]
        public void Delete_category()
        {
            var id = Guid.NewGuid();
            _helper
                .SetupFactoryToBeginWork()
                    .SetupCategoriesToDelete(id)
                .SetupWorkToCommit()
                .SetupWorkToDispose()
                .SetupCategoryDeletedEvent(new CategoryDeletedEvent { Id = id });

            //act
            _handler.Execute(new DeleteCategoryCommand { Id = id });
        }
    }
}

