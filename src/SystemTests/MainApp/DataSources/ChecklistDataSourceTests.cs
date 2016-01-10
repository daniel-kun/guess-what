using System;
using System.Collections.Generic;
using Io.GuessWhat.MainApp.Models;
using Xunit;

namespace Io.GuessWhat.SystemTests.MainApp.DataSources
{
    public class ChecklistDataSourceTests : IClassFixture <DbClassFixture>
    {
        public ChecklistDataSourceTests (DbClassFixture fixture)
        {
            mFixture = fixture;
        }

        [Fact]
        public void SavedChecklistResultModelShouldNotChangeWhenLoaded()
        {
            // sut = System Under Test
            var sut = new GuessWhat.MainApp.DataSources.ChecklistDataSource(mFixture.ChecklistDb);
            ChecklistResultModel demoData;
            demoData = CreateDemoChecklistResultModel();
            Assert.Null(demoData.Id);
            sut.SaveChecklistResultModel(demoData);
            Assert.NotNull(demoData.Id);
            // Create a second ChecklistDataSource to make sure that the data is stored persistently:
            var sut2 = new GuessWhat.MainApp.DataSources.ChecklistDataSource(mFixture.ChecklistDb);
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
                Id = GuessWhat.Tools.Web.ShortGuid.CreateGuid(),
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
        private static ChecklistResultModel CreateDemoChecklistResultModel()
        {
            var fakeChecklistId = GuessWhat.MainApp.DataSources.ChecklistDataSource.mFakeChecklistId;
            var fakeCheckList = GuessWhat.MainApp.DataSources.ChecklistDataSource.mFakeChecklists[fakeChecklistId];
            return new ChecklistResultModel()
            {
                CreationTime = DateTime.Now,
                TemplateId = fakeChecklistId,
                UserId = "d.albuschat",
                Results = new List<ChecklistResultItem>
                {
                    new ChecklistResultItem ()
                    {
                        Result = ChecklistResult.CheckedAndOk,
                        TemplateItemId = fakeCheckList.Items[0].Id,
                    },
                    new ChecklistResultItem ()
                    {
                        Result = ChecklistResult.CheckedAndNotOk,
                        TemplateItemId = fakeCheckList.Items[1].Id,
                    },
                    new ChecklistResultItem ()
                    {
                        Result = ChecklistResult.NotChecked,
                        TemplateItemId = fakeCheckList.Items[2].Id,
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

        private DbClassFixture mFixture;

    }
}
