using Case3Vitour.Services.TourPlanServices;
using Case3Vitour.Services.TourServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Case3Vitour.Controllers
{
    public class DefaultTourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ITourPlanService _tourPlanService;

        public DefaultTourController(ITourService tourService, ITourPlanService tourPlanService)
        {
            _tourService = tourService;
            _tourPlanService = tourPlanService;
        }

        public async Task<IActionResult> TourList(int page = 1)
        {
            
            ViewBag.Page = page;

            var values = await _tourService.GetTourCardListAsync();
            return View(values);
        }

        
        public async Task<IActionResult> TourDetails(string id)
        {
            
            var value = await _tourService.GetTourDetailAsync(id);

            
            ViewBag.Plans = await _tourPlanService.GetAllTourPlanByTourIdAsync(id);

            if (value == null)
            {
                return NotFound();
            }

            return View(value);
        }
    }
}