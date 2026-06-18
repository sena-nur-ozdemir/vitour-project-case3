using Case3Vitour.Dtos.GalleryDtos;
using Case3Vitour.Services.GalleryServices;
using Case3Vitour.Services.TourServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Case3Vitour.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryService _galleryService;
        private readonly ITourService _tourService;

        public GalleryController(IGalleryService galleryService, ITourService tourService)
        {
            _galleryService = galleryService;
            _tourService = tourService;
        }

        public async Task<IActionResult> GalleryList()
        {
            var values = await _galleryService.GetAllImagesAsync();

            // Listeleme sayfasında ID'leri isimlere dönüştürebilmek için turları SelectListItem olarak gönderiyoruz
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = tours.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.TourId
            }).ToList();

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateImage()
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = tours.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.TourId
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateImage(CreateGalleryDto createGalleryDto)
        {
            if (!ModelState.IsValid)
            {
                var tours = await _tourService.GetAllTourAsync();
                ViewBag.Tours = tours.Select(x => new SelectListItem { Text = x.Title, Value = x.TourId }).ToList();
                return View(createGalleryDto);
            }

            await _galleryService.CreateImageAsync(createGalleryDto);
            return RedirectToAction("GalleryList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImage(string id)
        {
            await _galleryService.DeleteImageAsync(id);
            return RedirectToAction("GalleryList");
        }

        // --- TOPLU SİLME MOTORU (Arayüzdeki yeşil bar için) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkDelete(string[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    await _galleryService.DeleteImageAsync(id);
                }
            }
            return RedirectToAction("GalleryList");
        }
    }
}