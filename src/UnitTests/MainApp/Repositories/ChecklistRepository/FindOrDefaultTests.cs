using System.Collections.Generic;
using Xunit;

namespace Io.GuessWhat.UnitTests.MainApp.Repositories.ChecklistRepository
{
    public class FindOrDefaultTests
    {
        [Fact]
        public void ShouldReturnDefaultWithEmptyIdWhenItemNotFound()
        {
            var emptyTemplateItems = new List<GuessWhat.MainApp.Models.ChecklistItem>();
            var result = GuessWhat.MainApp.Repositories.ChecklistRepository.FindOrDefault(emptyTemplateItems, "hello");
            Assert.NotNull(result);
            Assert.Equal(result.Id, string.Empty);
            // Do not check the title - it is arbitrary.
        }

        [Fact]
        public void ShouldReturnTemplateItemIfItemIsFound()
        {
            var emptyTemplateItems = new List<GuessWhat.MainApp.Models.ChecklistItem>()
            {
                new GuessWhat.MainApp.Models.ChecklistItem()
                {
                    Id = "foo",
                    Title = "Bar",
                }
            };
            var result = GuessWhat.MainApp.Repositories.ChecklistRepository.FindOrDefault(emptyTemplateItems, emptyTemplateItems[0].Id);
            Assert.Same(result, emptyTemplateItems[0]);
        }

    }
}
