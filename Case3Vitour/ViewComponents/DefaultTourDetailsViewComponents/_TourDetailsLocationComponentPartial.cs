using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Case3Vitour.Services.TourServices;

namespace Case3Vitour.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsLocationComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;

        public _TourDetailsLocationComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var value = await _tourService.GetTourByIdAsync(id);
            return View(value);
        }
    }
}