using System;
using Europa.Write.Messages;
using Xunit;

namespace Europa.Write.Handlers.Tests
{
    public class DeletePodcastHandlerTests : IDisposable
    {
        private readonly Helper _helper;
        private readonly DeletePodcastHandler _handler;

        public DeletePodcastHandlerTests()
        {
            _helper = new Helper();
            _handler = new DeletePodcastHandler(_helper.MockUnitOfWorkFactory.Object, _helper.MockEventDispatcher.Object);
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
                    .SetupPodcastsToDelete(id)
                .SetupWorkToCommit()
                .SetupWorkToDispose()
                .SetupPodcastDeletedEvent(new PodcastDeletedEvent { Id = id });

            //act
            _handler.Execute(new DeletePodcastCommand { Id = id });
        }
    }
}

