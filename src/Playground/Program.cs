using System;

namespace Io.GuessWhat.Playground
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Program
    {
        public static int Main(string[] args)
        {
            /*
            var client = new MongoDB.Driver.MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("checklist");
            var doc = new BsonDocument{
                { "Id", "asdf-asdf" },
            };
            var templates = db.GetCollection<BsonDocument>("templates");
            templates.InsertOne(doc);
            */
            Console.WriteLine("Hello, World!");
            Console.ReadLine();
            return 0;
        }
    }
}
