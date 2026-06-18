using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Case3Vitour.Entities
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReviewId { get; set; } = null!;

        public string TourId { get; set; } = null!;
        public string NameSurname { get; set; } = null!;
        public string Comment { get; set; } = null!;

        // Puanlama Alanları
        public int GuideScore { get; set; }
        public int AccommodationScore { get; set; }
        public int TransportationScore { get; set; }
        public int ValueForMoneyScore { get; set; }

        public double Score { get; set; } // Ortalamanın tutulacağı alan
        public bool Status { get; set; } = true;
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}