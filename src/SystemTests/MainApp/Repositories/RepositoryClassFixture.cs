using Io.GuessWhat.MainApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Xunit;

namespace Io.GuessWhat.SystemTests.MainApp.Repositories
{
    /**
    A fixture for all tests that access the database.
    It clears all data initially and after all tests of the derived class ran, making
    the test run on a "vanilla" database.
    **/
    public class RepositoryClassFixture : IDisposable
    {
        /// Initializes the properties Client, ChecklistDb and ResultsCollection.
        /// Removes all data from ResultsCollection.
        public RepositoryClassFixture()
        {
            RepositorySettings = new GuessWhat.MainApp.Repositories.Settings()
            {
                DbConnectionString = "mongodb://localhost:27017",
                DbDatabaseName = "checklist-tests",
            };
            Client = ConnectToMongo(RepositorySettings);
            ChecklistDb = GetChecklistDb(Client, RepositorySettings);
            ResultsCollection = GetResultsCollection(ChecklistDb);
            TemplatesCollection = GetTemplatesCollection(ChecklistDb);
            
            // Delete ALL documents from all collections initially, and assert that it worked:
            ResultsCollection.DeleteMany(new BsonDocument ());
            Assert.Empty(ResultsCollection.Find(new BsonDocument()).ToEnumerable ());
            TemplatesCollection.DeleteMany(new BsonDocument());
            Assert.Empty(TemplatesCollection.Find(new BsonDocument()).ToEnumerable());
        }

        /// Deletes all data from ResultsCollection.
        public void Dispose()
        {
            // Delete ALL documents from all collections after the tests, and assert that it worked:
            ResultsCollection.DeleteMany(new BsonDocument());
            Assert.Empty(ResultsCollection.Find(new BsonDocument()).ToEnumerable());
            TemplatesCollection.DeleteMany(new BsonDocument());
            Assert.Empty(TemplatesCollection.Find(new BsonDocument()).ToEnumerable());
        }

        public GuessWhat.MainApp.Repositories.Settings RepositorySettings
        {
            get;
            private set;
        }

        // An active MongoDB connection
        public MongoClient Client
        {
            get;
            private set;
        }

        // The checklists database from the Client connection
        public IMongoDatabase ChecklistDb
        {
            get;
            private set;
        }

        // The "results" collection from the ChecklistDb
        public IMongoCollection<ChecklistResultModel> ResultsCollection
        {
            get;
            private set;
        }

        // The "templates" collection from the ChecklistDb
        public IMongoCollection<ChecklistModel> TemplatesCollection
        {
            get;
            private set;
        }

        private static MongoClient ConnectToMongo(GuessWhat.MainApp.Repositories.Settings settings)
        {
            var client = new MongoClient(settings.DbConnectionString);
            return client;
        }

        private static IMongoDatabase GetChecklistDb(MongoClient client, GuessWhat.MainApp.Repositories.Settings settings)
        {
            return client.GetDatabase(settings.DbDatabaseName);
        }

        private static IMongoCollection<ChecklistResultModel> GetResultsCollection(IMongoDatabase checklistDb)
        {
            return checklistDb.GetCollection<ChecklistResultModel>("results");
        }

        private IMongoCollection<ChecklistModel> GetTemplatesCollection(IMongoDatabase checklistDb)
        {
            return checklistDb.GetCollection<ChecklistModel>("templates");
        }

        /// Tool functions:

        public static void AssertElementsAreEqual(ChecklistModel demoModel, ChecklistModel result)
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
        public static ChecklistModel CreateDemoChecklistModel()
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

        /**
        Runs some Asserts to check whether demoData is equal to result.
        **/
        public static void AssertElementsAreEqual(ChecklistResultModel demoData, ChecklistResultModel result)
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
        public static ChecklistModel CreateDemoChecklistTemplate()
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
        public static ChecklistResultModel CreateDemoChecklistResultModel(ChecklistModel template)
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
        public static DateTime RemoveMilliseconds(DateTime dateTime)
        {
            var utc = dateTime.ToUniversalTime();
            return new DateTime(utc.Year, utc.Month, utc.Day, utc.Hour, utc.Minute, utc.Second, DateTimeKind.Utc);
        }


    }
}
