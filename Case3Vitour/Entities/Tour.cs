using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Case3Vitour.Entities
{
    public class Tour
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TourId { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string DayNight { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public string Badge { get; set; } = null!;
        public bool Status { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string MapLocationImageUrl { get; set; } = null!;
        public string MapLocationTitle { get; set; } = null!;
        public string MapLocationDescription { get; set; } = null!;
    }
}