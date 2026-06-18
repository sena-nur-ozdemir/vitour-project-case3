namespace Case3Vitour.Dtos.TourDtos
{
    public class CreateTourDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;
        public string Badge { get; set; } = null!;
        public string DayNight { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; } = true;
        public string CategoryId { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string MapLocationImageUrl { get; set; } = null!;
        public string MapLocationTitle { get; set; } = null!;
        public string MapLocationDescription { get; set; } = null!;
    }
}