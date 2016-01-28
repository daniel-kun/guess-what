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
            var testData = new List<GuessWhat.MainApp.Models.ChecklistItem>()
            {
                new GuessWhat.MainApp.Models.ChecklistItem()
                {
                    Id = "foo",
                    Title = "Bar",
                }
            };
            var result = GuessWhat.MainApp.Repositories.ChecklistRepository.FindOrDefault(testData, testData[0].Id);
            Assert.Same(result, testData[0]);
        }

        [Fact]
        public void ShouldReturnTemplateItemIfItemIsGrandChild()
        {
            var testData = new List<GuessWhat.MainApp.Models.ChecklistItem>()
            {
                new GuessWhat.MainApp.Models.ChecklistItem()
                {
                    Id = "foo",
                    Title = "Bar",
                    Items = new List<GuessWhat.MainApp.Models.ChecklistItem>()
                    {
                        new GuessWhat.MainApp.Models.ChecklistItem()
                        {
                            Id = "child1",
                            Title = "Bar",
                        }
                    }
                },
                new GuessWhat.MainApp.Models.ChecklistItem()
                {
                    Id = "parent",
                    Title = "Bar",
                    Items = new List<GuessWhat.MainApp.Models.ChecklistItem>()
                    {
                        new GuessWhat.MainApp.Models.ChecklistItem()
                        {
                            Id = "child2",
                            Title = "Bar",
                            Items = new List<GuessWhat.MainApp.Models.ChecklistItem>()
                            {
                                new GuessWhat.MainApp.Models.ChecklistItem()
                                {
                                    Id = "grandchild",
                                    Title = "Bar",
                                }
                            }
                        }
                    }
                }
            };

            var result = GuessWhat.MainApp.Repositories.ChecklistRepository.FindOrDefault(testData, "grandchild");

            Assert.Same(result, testData[1].Items[0].Items[0]);
            Assert.Equal("grandchild", result.Id);
        }

    }
}
