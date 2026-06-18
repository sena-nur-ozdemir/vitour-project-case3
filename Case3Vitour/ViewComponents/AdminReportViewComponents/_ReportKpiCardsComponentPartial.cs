using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Services.ReportServices;

namespace Case3Vitour.ViewComponents.AdminReportViewComponents
{
    public class _ReportKpiCardsComponentPartial : ViewComponent
    {
        private readonly IReportService _reportService;

        public _ReportKpiCardsComponentPartial(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _reportService.GetKpiCardsAsync();
            return View(values);
        }
    }
}