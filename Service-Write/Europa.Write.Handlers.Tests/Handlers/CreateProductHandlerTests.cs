using System;
using Europa.Write.Messages;
using Xunit;

namespace Europa.Write.Handlers.Tests
{
    public class CreatePodcastHandlerTests : IDisposable
    {
        private readonly Helper _helper;
        private readonly CreatePodcastHandler _handler;

        public CreatePodcastHandlerTests()
        {
            _helper = new Helper();
            _handler = new CreatePodcastHandler(_helper.MockUnitOfWorkFactory.Object, 
                _helper.MockEventDispatcher.Object, _helper.MockCommandDispatcher.Object);
        }

        public void Dispose()
        {
            _helper.Verify();
        }

        [Fact]
        public void Create_Podcast_when_id_does_not_exist()
        {
            var id = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var link = "Podcast 1";

            _helper
                .SetupFactoryToBeginWork()
                .SetupPodcastToExist(id, false)
                .SetupPodcastsToCreate(id, link, categoryId)
                .SetupWorkToCommit()
                .SetupWorkToDispose()
                .SetupPodcastSavedEvent(new PodcastCreatedEvent { Id = id });

            //act
            _handler.Execute(new CreatePodcastCommand { Id = id, Link = link, CategoryId = categoryId });
        }

        [Fact]
        public void Create_Podcast_when_id_exists()
        {
            var id = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var link = "Podcast 1";

            _helper
                .SetupFactoryToBeginWork()
                .SetupPodcastToExist(id, true)
                .SetupWorkToDispose();

            //act
            _handler.Execute(new CreatePodcastCommand { Id = id, Link = link, CategoryId = categoryId });
        }
    }
}

