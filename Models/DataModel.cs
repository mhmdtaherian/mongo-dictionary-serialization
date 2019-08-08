using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoDictionarySerializtion.Models
{
    public class DataModel
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }

        [BsonSerializer(typeof(MongoDictionarySerializer))]
        public Dictionary<string, object> MyProperties { get; set; }

        public DataModel()
        {
            MyProperties = new Dictionary<string, object>();
        }
    }
}
