using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Services.TourServices;

namespace Case3Vitour.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsInformationComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;

        public _TourDetailsInformationComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var value = await _tourService.GetTourDetailAsync(id);
            return View(value);
        }
    }
}