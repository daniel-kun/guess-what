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
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);
            return Tuple.Create(client, db);
        }

        public string ConnectionString
        {
            get;
            set;
        }

        public string DatabaseName
        {
            get;
            set;
        }
    }

}
