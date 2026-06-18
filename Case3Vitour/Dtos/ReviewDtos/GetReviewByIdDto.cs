namespace Case3Vitour.Dtos.ReviewDtos
{
    public class GetReviewByIdDto
    {
        public string ReviewId { get; set; } = null!;
        public string NameSurname { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public double Score { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool Status { get; set; }
        public string TourId { get; set; } = null!;

        // Detaylı puanlama alanları 
        public int GuideScore { get; set; }
        public int TransportationScore { get; set; }
        public int AccommodationScore { get; set; }
        public int ValueForMoneyScore { get; set; }
    }
}
