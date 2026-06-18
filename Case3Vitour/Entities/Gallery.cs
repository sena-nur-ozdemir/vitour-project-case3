using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Case3Vitour.Entities
{
    public class Gallery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GalleryId { get; set; } = null!;

        public string TourId { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}