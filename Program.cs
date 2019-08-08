using MongoDB.Driver;
using MongoDictionarySerializtion.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MongoDictionarySerializtion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            //Preparing connection to DB
            var ConnectionString = "mongodb://localhost:27017";
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase("TestDB");
            var collection = db.GetCollection<DataModel>("DictCollection");

            //I'm just using absolute path for convenience
            var objStr = File.ReadAllText("E:\\WorkSpace\\MongoDictionarySerializtion\\MongoDictionarySerializtion\\Models\\json1.json");
            var myObj = JsonConvert.DeserializeObject<DataModel>(objStr);

            //Insert object to DB
            collection.InsertOne(myObj);

            Console.WriteLine("One document was inserted. Press any key to read it...");
            Console.ReadKey();

            //Read object from DB
            myObj = collection.Find(Builders<DataModel>.Filter.Empty).FirstOrDefault();

            Console.WriteLine(JsonConvert.SerializeObject(myObj));

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
