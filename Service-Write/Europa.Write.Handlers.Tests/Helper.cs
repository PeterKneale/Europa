using System;
using System.Collections.Generic;
using Europa.Write.Data;
using Europa.Write.Data.DataSources;
using Moq;
using Europa.Infrastructure;
using Europa.Write.Messages;
using System.Threading.Tasks;

namespace Europa.Write.Handlers.Tests
{
    public class Helper
    {
        public readonly MockRepository StrictRepo;
        public readonly MockRepository LooseRepo;
        public readonly Mock<IUnitOfWorkFactory> MockUnitOfWorkFactory;
        public readonly Mock<IUnitOfWork> MockUnitOfWOrk;
        public readonly Mock<ICategoryDataSource> MockCategories;
        public readonly Mock<IPodcastDataSource> MockPodcasts;
        public readonly Mock<IEventDispatcher> MockEventDispatcher;
        public readonly Mock<ICommandDispatcher> MockCommandDispatcher;

        public Helper()
        {
            StrictRepo = new MockRepository(MockBehavior.Strict);
            LooseRepo = new MockRepository(MockBehavior.Loose);

            MockCategories = StrictRepo.Create<ICategoryDataSource>();
            MockPodcasts = StrictRepo.Create<IPodcastDataSource>();
            MockUnitOfWorkFactory = StrictRepo.Create<IUnitOfWorkFactory>();
            MockEventDispatcher = StrictRepo.Create<IEventDispatcher>();
            MockCommandDispatcher = StrictRepo.Create<ICommandDispatcher>();

            // Loose behaviour because these tests are more focused on the correct order of calls to the repositories
            MockUnitOfWOrk = LooseRepo.Create<IUnitOfWork>();
            
            MockUnitOfWOrk
                .Setup(x => x.Categories)
                .Returns(MockCategories.Object);

            MockUnitOfWOrk
                .Setup(x => x.Podcasts)
                .Returns(MockPodcasts.Object);
        }

        public Helper SetupFactoryToCreateWork()
        {
            MockUnitOfWorkFactory.Setup(x => x.Create())
                .Returns(MockUnitOfWOrk.Object)
                .Verifiable();
            return this;
        }

        public Helper SetupFactoryToBeginWork()
        {
            MockUnitOfWorkFactory.Setup(x => x.Begin())
                .Returns(MockUnitOfWOrk.Object)
                .Verifiable();
            return this;
        }
        
        public Helper SetupListToReturn(IEnumerable<Category> categories)
        {
            MockCategories
                .Setup(x => x.List())
                .Returns(categories)
                .Verifiable();
            return this;
        }

        public Helper SetupCategoriesToCreate(Guid id, string name)
        {
            MockCategories.Setup(x => x.Save(It.Is<Category>(c => c.Id == id && c.Name == name)))
                .Verifiable();
            return this;
        }

        public Helper SetupCategoriesToUpdate(Guid id, string name)
        {
            MockCategories.Setup(x => x.Update(It.Is<Category>(c => c.Id == id && c.Name == name)))
               .Verifiable();
            return this;
        }

        public Helper SetupPodcastsToCreate(Guid id, string name, Guid categoryId)
        {
            MockPodcasts.Setup(x => x.Save(It.Is<Podcast>(c => 
                    c.Id == id 
                    && c.Title == name 
                    && c.CategoryId == categoryId)))
                .Verifiable();
            return this;
        }

        public Helper SetupPodcastsToUpdate(Guid id, string name, Guid categoryId)
        {
            MockPodcasts.Setup(x => x.Update(It.Is<Podcast>(c => 
            c.Id == id 
            && c.Title == name 
            && c.CategoryId == categoryId)))
               .Verifiable();
            return this;
        }

        public Helper SetupCategoriesToGet(Guid id, Category category)
        {
            MockCategories.Setup(x => x.Get(It.Is<Guid>(i => i == id)))
               .Returns(category)
               .Verifiable();
            return this;
        }

        public Helper SetupPodcastToGet(Guid id, Podcast Podcast)
        {
            MockPodcasts.Setup(x => x.Get(It.Is<Guid>(i => i == id)))
               .Returns(Podcast)
               .Verifiable();
            return this;
        }

        public Helper SetupCategoriesToExist(Guid id, bool exists)
        {
            MockCategories.Setup(x => x.Exists(id))
                .Returns(exists)
                .Verifiable();
            return this;
        }

        public Helper SetupPodcastToExist(Guid id, bool exists)
        {
            MockPodcasts.Setup(x => x.Exists(id))
                .Returns(exists)
                .Verifiable();
            return this;
        }
        
        public Helper SetupCategoriesToDelete(Guid id)
        {
            MockCategories.Setup(x => x.Delete(id))
                .Verifiable();
            return this;
        }

        public Helper SetupPodcastsToDelete(Guid id)
        {
            MockPodcasts.Setup(x => x.Delete(id))
                .Verifiable();
            return this;
        }


        public Helper SetupWorkToCommit()
        {
            MockUnitOfWOrk.Setup(x => x.Commit())
                .Verifiable();
            return this;
        }

        public Helper SetupWorkToDispose()
        {
            MockUnitOfWOrk.Setup(x => x.Dispose())
                .Verifiable();
            return this;
        }

        public Helper SetupCategorySavedEvent(CategoryCreatedEvent evnt)
        {
            MockEventDispatcher
                .Setup(x => x.Publish(It.Is<CategoryCreatedEvent>(s => s.Id == evnt.Id)))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }


        public Helper SetupPodcastSavedEvent(PodcastCreatedEvent evnt)
        {
            MockEventDispatcher
                .Setup(x => x.Publish(It.Is<PodcastCreatedEvent>(s => s.Id == evnt.Id)))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }

        public Helper SetupPodcastDeletedEvent(PodcastDeletedEvent evnt)
        {
            MockEventDispatcher
                .Setup(x => x.Publish(It.Is<PodcastDeletedEvent>(s => s.Id == evnt.Id)))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }

        public Helper SetupCategoryDeletedEvent(CategoryDeletedEvent evnt)
        {
            MockEventDispatcher
                .Setup(x => x.Publish(It.Is<CategoryDeletedEvent>(s => s.Id == evnt.Id)))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }


        public void Verify()
        {
            StrictRepo.VerifyAll();
            LooseRepo.Verify();
        }
    }
}

