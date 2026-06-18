namespace Case3Vitour.Dtos.ReportDtos
{
    public class KpiCardDto
    {
        public string Title { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string PreviousValue { get; set; } = null!;
        public double PercentageChange { get; set; }
        public bool IsIncrease { get; set; }
    }
}