using Case3Vitour.Dtos.ReportDtos;

namespace Case3Vitour.Services.ReportServices
{
    public interface IReportService
    {
        Task<List<KpiCardDto>> GetKpiCardsAsync();
        Task<List<MonthlyRevenueDto>> GetMonthlyRevenueChartAsync();
        Task<List<ResultTopTourDto>> GetTopTourListAsync();
        Task<List<MonthlyGoalDto>> GetMonthlyGoalsAsync();
    }
}
