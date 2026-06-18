namespace Case3Vitour.Dtos.TourPlanDtos
{
    public class CreateTourPlanDto
    {
        public int DayNumber { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TourId { get; set; } = null!;
    }
}