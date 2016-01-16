using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Io.GuessWhat.SystemTests.MainApp.Repositories.ChecklistRepository
{
    public class LoadChecklistBrowseItemsTests : IClassFixture<RepositoryClassFixture>
    {
        private RepositoryClassFixture mFixture;

        public LoadChecklistBrowseItemsTests(RepositoryClassFixture fixture)
        {
            mFixture = fixture;
        }

        [Fact]
        public void ShouldLoadOnlyMaxBrowseItems()
        {
            var sut = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            for (int i = 0; i < GuessWhat.MainApp.Repositories.ChecklistRepository.MaxBrowseItems + 5; ++i)
            {
                sut.SaveChecklistModel(RepositoryClassFixture.CreateDemoChecklistModel());
            }
            var browseItems = sut.LoadChecklistBrowseItems().ToList();
            Assert.Equal(GuessWhat.MainApp.Repositories.ChecklistRepository.MaxBrowseItems, browseItems.Count);
        }
    }
}
