namespace Case3Vitour.Dtos.TourDtos
{
    public class TourCardDto
    {
        public string TourId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;
        public string DayNight { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; } = null!;
        public string? CategoryName { get; set; }

        public string? Badge { get; set; }

        // Kart üzerinde görünen istatistikler
        public int ReviewCount { get; set; }
        public double ReviewScore { get; set; }
        public int GalleryCount { get; set; }
    }
}