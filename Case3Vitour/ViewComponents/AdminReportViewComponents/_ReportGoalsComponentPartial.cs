using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Services.ReportServices;

namespace Case3Vitour.ViewComponents.AdminReportViewComponents
{
    public class _ReportGoalsComponentPartial : ViewComponent
    {
        private readonly IReportService _reportService;

        public _ReportGoalsComponentPartial(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _reportService.GetMonthlyGoalsAsync();
            return View(values);
        }
    }
}