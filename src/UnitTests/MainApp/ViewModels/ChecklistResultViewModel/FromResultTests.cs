using System.Collections.Generic;
using Xunit;

namespace Io.GuessWhat.UnitTests.MainApp.ViewModels.ChecklistResultViewModel
{
    public class FromResultTests
    {
        [Fact]
        public void ShouldConnectItemsWhenTheTemplateIsFlat()
        {
            var testData = new GuessWhat.MainApp.Models.ChecklistResultModel()
            {
                Template = new GuessWhat.MainApp.Models.ChecklistModel()
                {
                    Items = new List<GuessWhat.MainApp.Models.ChecklistItem>
                    {
                        new GuessWhat.MainApp.Models.ChecklistItem ()
                        {
                            Id = "1",
                            Title = "test1"
                        },
                        new GuessWhat.MainApp.Models.ChecklistItem ()
                        {
                            Id = "2",
                            Title = "test2"
                        },
                        new GuessWhat.MainApp.Models.ChecklistItem ()
                        {
                            Id = "3",
                            Title = "test3"
                        },
                    }
                },
                Results = new List<GuessWhat.MainApp.Models.ChecklistResultItem>
                {
                    new GuessWhat.MainApp.Models.ChecklistResultItem ()
                    {
                        TemplateItemId = "1",
                        Result = GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                    },
                    new GuessWhat.MainApp.Models.ChecklistResultItem ()
                    {
                        TemplateItemId = "2",
                        Result = GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk,
                    },
                    new GuessWhat.MainApp.Models.ChecklistResultItem ()
                    {
                        TemplateItemId = "3",
                        Result = GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                    },
                }
            };

            var result = GuessWhat.MainApp.ViewModels.ChecklistResultViewModel.FromResult(testData);

            Assert.Equal(3, result.Items.Count);
            Assert.Same(testData.Template.Items[0], result.Items[0].TemplateItem);
            Assert.Equal("1", result.Items[0].TemplateItem.Id);
            Assert.Equal("test1", result.Items[0].TemplateItem.Title);
            Assert.Equal(GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk, result.Items[0].ResultItem.Result);

            Assert.Same(testData.Template.Items[1], result.Items[1].TemplateItem);
            Assert.Equal("2", result.Items[1].TemplateItem.Id);
            Assert.Equal("test2", result.Items[1].TemplateItem.Title);
            Assert.Equal(GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk, result.Items[1].ResultItem.Result);

            Assert.Same(testData.Template.Items[2], result.Items[2].TemplateItem);
            Assert.Equal("3", result.Items[2].TemplateItem.Id);
            Assert.Equal("test3", result.Items[2].TemplateItem.Title);
            Assert.Equal(GuessWhat.MainApp.Models.ChecklistResult.NotChecked, result.Items[2].ResultItem.Result);
        }

        [Fact]
        public void ShouldConnectHierarchicalItemsAndPreserveHierarchy()
        {
            var testData = new GuessWhat.MainApp.Models.ChecklistResultModel()
            {
                Template = new GuessWhat.MainApp.Models.ChecklistModel()
                {
                    Items = new List<GuessWhat.MainApp.Models.ChecklistItem>
                    {
                        new GuessWhat.MainApp.Models.ChecklistItem ()
                        {
                            Id = "1",
                            Title = "test1",
                            Items = new List<GuessWhat.MainApp.Models.ChecklistItem>
                            {
                                new GuessWhat.MainApp.Models.ChecklistItem ()
                                {
                                    Id = "1.1",
                                    Title = "test child 1.1"
                                },
                                new GuessWhat.MainApp.Models.ChecklistItem ()
                                {
                                    Id = "1.2",
                                    Title = "test child 1.2"
                                },
                            }
                        },
                        new GuessWhat.MainApp.Models.ChecklistItem ()
                        {
                            Id = "2",
                            Title = "test2",
                        }
                    }
                },
                Results = new List<GuessWhat.MainApp.Models.ChecklistResultItem>
                {
                    new GuessWhat.MainApp.Models.ChecklistResultItem ()
                    {
                        TemplateItemId = "1",
                        Result = GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                    },
                    new GuessWhat.MainApp.Models.ChecklistResultItem ()
                    {
                        TemplateItemId = "1.1",
                        Result = GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk,
                    },
                    new GuessWhat.MainApp.Models.ChecklistResultItem ()
                    {
                        TemplateItemId = "1.2",
                        Result = GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                    },
                    new GuessWhat.MainApp.Models.ChecklistResultItem ()
                    {
                        TemplateItemId = "2",
                        Result = GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                    },
                }
            };

            var result = GuessWhat.MainApp.ViewModels.ChecklistResultViewModel.FromResult(testData);

            Assert.Equal(2, result.Items.Count);
            Assert.Same(testData.Template.Items[0], result.Items[0].TemplateItem);
            Assert.Equal("1", result.Items[0].TemplateItem.Id);
            Assert.Equal("test1", result.Items[0].TemplateItem.Title);
            Assert.Equal(GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk, result.Items[0].ResultItem.Result);

            Assert.Equal(2, result.Items[0].Items.Count);

            Assert.Same(testData.Template.Items[0].Items[0], result.Items[0].Items[0].TemplateItem);
            Assert.Equal("1.1", result.Items[0].Items[0].TemplateItem.Id);
            Assert.Equal("test child 1.1", result.Items[0].Items[0].TemplateItem.Title);
            Assert.Equal(GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk, result.Items[0].Items[0].ResultItem.Result);

            Assert.Same(testData.Template.Items[0].Items[1], result.Items[0].Items[1].TemplateItem);
            Assert.Equal("1.2", result.Items[0].Items[1].TemplateItem.Id);
            Assert.Equal("test child 1.2", result.Items[0].Items[1].TemplateItem.Title);
            Assert.Equal(GuessWhat.MainApp.Models.ChecklistResult.NotChecked, result.Items[0].Items[1].ResultItem.Result);

            Assert.Same(testData.Template.Items[1], result.Items[1].TemplateItem);
            Assert.Equal("2", result.Items[1].TemplateItem.Id);
            Assert.Equal("test2", result.Items[1].TemplateItem.Title);
            Assert.Equal(GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk, result.Items[1].ResultItem.Result);
        }

    }
}
