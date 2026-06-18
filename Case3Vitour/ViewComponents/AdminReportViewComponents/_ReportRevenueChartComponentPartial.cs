using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Services.ReportServices;

namespace Case3Vitour.ViewComponents.AdminReportViewComponents
{
    public class _ReportRevenueChartComponentPartial : ViewComponent
    {
        private readonly IReportService _reportService;

        public _ReportRevenueChartComponentPartial(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _reportService.GetMonthlyRevenueChartAsync();
            return View(values);
        }
    }
}