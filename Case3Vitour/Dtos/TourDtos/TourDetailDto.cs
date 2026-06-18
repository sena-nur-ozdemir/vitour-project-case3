namespace Case3Vitour.Dtos.TourDtos
{
    public class TourDetailDto
    {
        public string TourId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;
        public string DayNight { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public string? CategoryName { get; set; }

        public string? Badge { get; set; }
        public int ReviewCount { get; set; }
        public double ReviewScore { get; set; }

        // Rota Konumu Sekmesi için
        public string MapLocationImageUrl { get; set; } = null!;
        public string MapLocationTitle { get; set; } = null!;
        public string MapLocationDescription { get; set; } = null!;
    }
}