namespace Case3Vitour.Dtos.ReportDtos
{
    public class MonthlyGoalDto
    {
        public string Name { get; set; } = null!;
        public decimal CurrentValue { get; set; }
        public decimal TargetValue { get; set; }
        public string Unit { get; set; } = null!; // TL, Kişi vb.
    }
}
