using Io.GuessWhat.MainApp.Models;
using System.Linq;
using Xunit;

namespace Io.GuessWhat.SystemTests.MainApp.Repositories.ChecklistRepository
{
    public class SaveChecklistModelTests : IClassFixture<RepositoryClassFixture>
    {
        private RepositoryClassFixture mFixture;

        public SaveChecklistModelTests(RepositoryClassFixture fixture)
        {
            mFixture = fixture;
        }

        [Fact]
        public void SavedChecklistModelShouldNotChangeWhenLoaded()
        {
            var sut = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            ChecklistModel demoModel = RepositoryClassFixture.CreateDemoChecklistModel();
            Assert.Null(demoModel.Id);
            Assert.DoesNotContain(demoModel.Items, item => item.Id != null);
            demoModel = sut.SaveChecklistModel(demoModel);
            Assert.NotNull(demoModel.Id);
            Assert.DoesNotContain(demoModel.Items, item => item.Id == null);
            // Create a second ChecklistDataSource to make sure that the data is stored persistently:
            var sut2 = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            var result = sut2.LoadChecklistModel(demoModel.Id);
            RepositoryClassFixture.AssertElementsAreEqual(demoModel, result);
        }

        [Fact]
        public void SavedChecklistModelShouldExistInBrowse()
        {
            var sut = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            ChecklistModel demoModel = RepositoryClassFixture.CreateDemoChecklistModel();
            Assert.Null(demoModel.Id);
            Assert.DoesNotContain(demoModel.Items, item => item.Id != null);
            demoModel = sut.SaveChecklistModel(demoModel);
            Assert.NotNull(demoModel.Id);
            Assert.DoesNotContain(demoModel.Items, item => item.Id == null);
            var sut2 = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            var loadedModels = sut2.LoadChecklistBrowseItems();
            var singleItemList = loadedModels.Where(item => item.Id == demoModel.Id).ToList();
            Assert.Equal(1, singleItemList.Count);
            Assert.Equal(demoModel.Id, singleItemList[0].Id);
            Assert.Equal(demoModel.Title, singleItemList[0].Title);
        }

    }
}
