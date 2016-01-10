using Io.GuessWhat.MainApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using Xunit;

namespace Io.GuessWhat.SystemTests.MainApp.DataSources
{
    /**
    A fixture for all tests that access the database.
    It clears all data initially and after all tests of the derived class ran, making
    the test run on a "vanilla" database.
    **/
    public class DbClassFixture : IDisposable
    {
        /// Initializes the properties Client, ChecklistDb and ResultsCollection.
        /// Removes all data from ResultsCollection.
        public DbClassFixture()
        {
            Client = ConnectToMongo();
            ChecklistDb = GetChecklistDb(Client);
            ResultsCollection = GetResultsCollection(ChecklistDb);
            
            // Delete ALL results initially, and assert that it worked:
            ResultsCollection.DeleteMany(new BsonDocument ());
            Assert.Empty(ResultsCollection.Find(new BsonDocument()).ToEnumerable ());
        }

        /// Deletes all data from ResultsCollection.
        public void Dispose()
        {
            // Delete ALL results after the tests, and assert that it worked:
            ResultsCollection.DeleteMany(new BsonDocument());
            Assert.Empty(ResultsCollection.Find(new BsonDocument()).ToEnumerable());
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

        private static MongoClient ConnectToMongo()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            return client;
        }

        private static IMongoDatabase GetChecklistDb(MongoClient client)
        {
            return client.GetDatabase("checklist");
        }

        private static IMongoCollection<ChecklistResultModel> GetResultsCollection(IMongoDatabase db)
        {
            return db.GetCollection<ChecklistResultModel>("results");
        }

    }
}
