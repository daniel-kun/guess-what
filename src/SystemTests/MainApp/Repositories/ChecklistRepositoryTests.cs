using System;
using System.Linq;
using System.Collections.Generic;
using Io.GuessWhat.MainApp.Models;
using Xunit;

namespace Io.GuessWhat.SystemTests.MainApp.Repositories
{
    public class ChecklistRepositoryTests : IClassFixture <RepositoryClassFixture>
    {
        public ChecklistRepositoryTests (RepositoryClassFixture fixture)
        {
            mFixture = fixture;
        }

        [Fact]
        public void SavedChecklistModelShouldNotChangeWhenLoaded()
        {
            var sut = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            ChecklistModel demoModel = CreateDemoChecklistModel();
            Assert.Null(demoModel.Id);
            Assert.DoesNotContain(demoModel.Items, item => item.Id != null);
            demoModel = sut.SaveChecklistModel(demoModel);
            Assert.NotNull(demoModel.Id);
            Assert.DoesNotContain(demoModel.Items, item => item.Id == null);
            // Create a second ChecklistDataSource to make sure that the data is stored persistently:
            var sut2 = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            var result = sut2.LoadChecklistModel(demoModel.Id);
            AssertElementsAreEqual(demoModel, result);
        }

        [Fact]
        public void SavedChecklistModelShouldExistInBrowse()
        {
            var sut = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            ChecklistModel demoModel = CreateDemoChecklistModel();
            Assert.Null(demoModel.Id);
            Assert.DoesNotContain(demoModel.Items, item => item.Id != null);
            demoModel = sut.SaveChecklistModel(demoModel);
            Assert.NotNull(demoModel.Id);
            Assert.DoesNotContain(demoModel.Items, item => item.Id == null);
            var sut2 = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings>(mFixture.RepositorySettings));
            var loadedModels = sut2.LoadChecklistModelCollection();
            var singleItemList = loadedModels.Select(item => item).Where(item => item.Id == demoModel.Id).ToList();
            Assert.Equal(singleItemList.Count, 1);
            Assert.Equal(singleItemList[0].Id, demoModel.Id);
            Assert.Equal(singleItemList[0].Title, demoModel.Title);
        }

        private void AssertElementsAreEqual(ChecklistModel demoModel, ChecklistModel result)
        {
            Assert.Equal(demoModel.Id, result.Id);
            Assert.Equal(demoModel.Title, result.Title);
            Assert.Equal(demoModel.Description, result.Description);
            Assert.NotNull(demoModel.Items);
            Assert.NotNull(result.Items);
            if (demoModel.Items != null && result.Items != null)
            {
                Assert.Equal(demoModel.Items.Count, result.Items.Count);
                if (demoModel.Items.Count == result.Items.Count)
                {
                    for (int i = 0; i < demoModel.Items.Count; ++i)
                    {
                        var demoItem = demoModel.Items[i];
                        var resultItem = result.Items[i];
                        Assert.Equal(demoItem.Id, resultItem.Id);
                        Assert.Equal(demoItem.Title, resultItem.Title);
                    }
                }
            }
        }

        /// Creates a new dummy ChecklistModel with at least 3 items.
        private ChecklistModel CreateDemoChecklistModel()
        {
            return new ChecklistModel()
            {
                Title = "Awesome checklist",
                Description = "Lorem ipsum",
                Items = new List<ChecklistItem>
                {
                    new ChecklistItem ()
                    {
                        Title = "Checklist point 1",
                    },
                    new ChecklistItem ()
                    {
                        Title = "Checklist point 2",
                    },
                    new ChecklistItem ()
                    {
                       Title = "Checklist point 3",
                    },
                },
            };
        }

        [Fact]
        public void SavedChecklistResultModelShouldNotChangeWhenLoaded()
        {
            // sut = System Under Test
            var sut = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings> (mFixture.RepositorySettings));
            ChecklistResultModel demoData;
            demoData = CreateDemoChecklistResultModel(sut.SaveChecklistModel(CreateDemoChecklistModel()));
            Assert.Null(demoData.Id);
            demoData = sut.SaveChecklistResultModel(demoData);
            Assert.NotNull(demoData.Id);
            // Create a second ChecklistDataSource to make sure that the data is stored persistently:
            var sut2 = new GuessWhat.MainApp.Repositories.ChecklistRepository(
                new Mocking.Options<GuessWhat.MainApp.Repositories.Settings> (mFixture.RepositorySettings));
            var result = sut2.LoadChecklistResultModel(demoData.Id);
            AssertElementsAreEqual(demoData, result);
        }

        /**
        Runs some Asserts to check whether demoData is equal to result.
        **/
        private void AssertElementsAreEqual(ChecklistResultModel demoData, ChecklistResultModel result)
        {
            Assert.Equal(demoData.Id, result.Id);
            Assert.Equal(demoData.TemplateId, result.TemplateId);
            Assert.Equal(demoData.UserId, result.UserId);
            Assert.Equal(RemoveMilliseconds(demoData.CreationTime), RemoveMilliseconds(result.CreationTime));
            Assert.NotNull(demoData.Results);
            Assert.NotNull(result.Results);
            if (demoData.Results != null && result.Results != null)
            {
                Assert.Equal(demoData.Results.Count, result.Results.Count);
                if (demoData.Results.Count == result.Results.Count)
                {
                    for (int i = 0; i < demoData.Results.Count; ++i)
                    {
                        var demoItem = demoData.Results[i];
                        var resultItem = demoData.Results[i];
                        Assert.Equal(demoItem.Result, resultItem.Result);
                        Assert.Equal(demoItem.TemplateItemId, resultItem.TemplateItemId);
                    }
                }
            }
        }

        /**
        Creates a new checklist template with 3 items.
        **/
        private static ChecklistModel CreateDemoChecklistTemplate()
        {
            return new ChecklistModel()
            {
                Id = Tools.Web.ShortGuid.CreateGuid(),
                Description = "Lorem ipsum",
                Title = "Lorem ipsum",
                Items = new List<ChecklistItem>
                {
                    new ChecklistItem ()
                    {
                        Id = Tools.Web.ShortGuid.CreateGuid(),
                        Title = "Checklist Item 1",
                    },
                    new ChecklistItem ()
                    {
                        Id = Tools.Web.ShortGuid.CreateGuid(),
                        Title = "Checklist Item 2",
                    },
                    new ChecklistItem ()
                    {
                        Id = Tools.Web.ShortGuid.CreateGuid(),
                        Title = "Checklist Item 3",
                    },
                }
            };
        }

        /**
        Creates a demo result model with some random and some fixed infos.
        **/
        private static ChecklistResultModel CreateDemoChecklistResultModel(ChecklistModel template)
        {
            Assert.True(template.Items.Count >= 3);
            return new ChecklistResultModel()
            {
                CreationTime = DateTime.Now,
                TemplateId = template.Id,
                UserId = "d.albuschat",
                Results = new List<ChecklistResultItem>
                {
                    new ChecklistResultItem ()
                    {
                        Result = ChecklistResult.CheckedAndOk,
                        TemplateItemId = template.Items[0].Id,
                    },
                    new ChecklistResultItem ()
                    {
                        Result = ChecklistResult.CheckedAndNotOk,
                        TemplateItemId = template.Items[1].Id,
                    },
                    new ChecklistResultItem ()
                    {
                        Result = ChecklistResult.NotChecked,
                        TemplateItemId = template.Items[2].Id,
                    },
                }
            };
        }

        // Converts dateTime to UTC and removes the milliseconds part.
        private static DateTime RemoveMilliseconds(DateTime dateTime)
        {
            var utc = dateTime.ToUniversalTime();
            return new DateTime(utc.Year, utc.Month, utc.Day, utc.Hour, utc.Minute, utc.Second, DateTimeKind.Utc);
        }

        private RepositoryClassFixture mFixture;

    }
}
