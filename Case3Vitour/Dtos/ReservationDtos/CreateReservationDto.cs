namespace Case3Vitour.Dtos.ReservationDtos
{
    public class CreateReservationDto
    {
        // Yeni rezervasyon oluştururken (Ziyaretçi tarafı)
        public string NameSurname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int PersonCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public string ReservationCode { get; set; } = null!;
        public string TourId { get; set; } = null!;
    }
}
