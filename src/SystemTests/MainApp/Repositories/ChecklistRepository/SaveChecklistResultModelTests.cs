using Io.GuessWhat.MainApp.Models;
using Xunit;

namespace Io.GuessWhat.SystemTests.MainApp.Repositories.ChecklistRepository
{
    public class SaveChecklistResultModelTests : IClassFixture<RepositoryClassFixture>
    {
        private RepositoryClassFixture mFixture;

        public SaveChecklistResultModelTests(RepositoryClassFixture fixture)
        {
            mFixture = fixture;
        }

        [Fact]
        public void SavedChecklistResultModelShouldNotChangeWhenLoaded()
        {
            // sut = System Under Test
            var sut = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            ChecklistResultModel demoData;
            demoData = RepositoryClassFixture.CreateDemoChecklistResultModel(sut.SaveChecklistModel(RepositoryClassFixture.CreateDemoChecklistModel()));
            Assert.Null(demoData.Id);
            demoData = sut.SaveChecklistResultModel(demoData);
            Assert.NotNull(demoData.Id);
            // Create a second ChecklistDataSource to make sure that the data is stored persistently:
            var sut2 = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            var result = sut2.LoadChecklistResultModel(demoData.Id);
            RepositoryClassFixture.AssertElementsAreEqual(demoData, result);
        }

    }
}
