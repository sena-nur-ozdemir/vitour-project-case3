namespace Case3Vitour.Dtos.ReservationDtos
{
    public class GetReservationByIdDto
    {
        public string ReservationId { get; set; } = null!;
        public string NameSurname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int PersonCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ReservationDate { get; set; }
        public string ReservationCode { get; set; } = null!;
        public string TourId { get; set; } = null!;
    }
}
