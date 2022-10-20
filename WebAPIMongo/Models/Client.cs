using MongoDB.Bson.Serialization.Attributes;

namespace WebAPIMongo.Models
{
    public class Client
    {
        [BsonId] //informando minha api que é do tipo json 
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

    }
}
