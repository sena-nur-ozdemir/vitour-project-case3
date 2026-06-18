using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Case3Vitour.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public bool CategoryStatus { get; set; } = true;

    }
}
