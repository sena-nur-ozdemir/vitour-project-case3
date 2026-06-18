using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Case3Vitour.Entities
{
    public class TourPlan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TourPlanId { get; set; } = null!;

        public string TourId { get; set; } = null!;
        public int DayNumber { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}