namespace Case3Vitour.Dtos.TourDtos
{
    public class ResultTourDto
    {
        public string TourId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;
        public string Badge { get; set; } = null!; // Örn: "Popüler", "Yeni"
        public string DayNight { get; set; } = null!; // "3 Gün 2 Gece"
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public string CategoryId { get; set; } = null!;
        public string? CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }

        // Harita ve Konum Bilgileri
        public string MapLocationImageUrl { get; set; } = null!;
        public string MapLocationTitle { get; set; } = null!;
        public string MapLocationDescription { get; set; } = null!;
    }
}