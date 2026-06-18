using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Services.TourServices;

namespace Case3Vitour.ViewComponents.DefaultTourViewComponents
{
    public class _AllTourListComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;

        public _AllTourListComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        // PARAMETRELERİ YAKALIYORUZ
        public async Task<IViewComponentResult> InvokeAsync(int page = 1)
        {
            // URL'deki filtre parametrelerini yakala
            string categoryId = HttpContext.Request.Query["categoryId"];

            int pageSize = 6;

            // Servise categoryId'yi de gönderiyoruz
            var (tours, totalCount) = await _tourService.GetPagedToursAsync(page, pageSize, categoryId);

            ViewBag.CurrentPage = page;
            ViewBag.TotalCount = totalCount;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CategoryId = categoryId; // Sayfalama linkleri için tutuyoruz

            return View(tours);
        }
    }
}