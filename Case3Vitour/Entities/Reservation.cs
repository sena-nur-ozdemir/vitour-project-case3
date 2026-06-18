using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Case3Vitour.Entities
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReservationId { get; set; } = null!;

        public string TourId { get; set; } = null!;
        public string NameSurname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int PersonCount { get; set; }
        public decimal TotalPrice { get; set; } 
        public string ReservationCode { get; set; } = null!; 
        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Beklemede"; 
    }
}