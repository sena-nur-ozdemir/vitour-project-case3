using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Case3Vitour.Services.ReviewServices;

namespace Case3Vitour.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsReviewOverviewComponentPartial : ViewComponent
    {
        private readonly IReviewService _reviewService;

        public _TourDetailsReviewOverviewComponentPartial(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            
            var values = await _reviewService.GetReviewSummaryByTourIdAsync(id);
            return View(values);
        }
    }
}