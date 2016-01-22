using MongoDB.Driver;
using System;

namespace Io.GuessWhat.MainApp.Repositories
{
    public class Settings
    {

        /**
        Connects to the MongoDB server according to settings and creates/opens the database named databaseName.
        **/
        public static Tuple <MongoClient, IMongoDatabase> ConnectToDatabase (Settings settings)
        {
            var client = new MongoClient(settings.DbConnectionString);
            var db = client.GetDatabase(settings.DbDatabaseName);
            return Tuple.Create(client, db);
        }

        public string DbConnectionString
        {
            get;
            set;
        }

        public string DbDatabaseName
        {
            get;
            set;
        }
    }

}
