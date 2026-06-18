using Case3Vitour.Dtos.TourDtos;
using Case3Vitour.Services.CategoryServices;
using Case3Vitour.Services.TourServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Case3Vitour.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ICategoryService _categoryService;

        public TourController(ITourService tourService, ICategoryService categoryService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> TourList()
        {
            var values = await _tourService.GetAllTourWithCategoryAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTour()
        {
            var categories = await _categoryService.GetActiveCategoriesAsync();
            ViewBag.Categories = categories.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryId
            }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTour(CreateTourDto createTourDto)
        {
            // Eğer formdan harita linki girilmezse varsayılan bir değer atıyoruz.
            createTourDto.MapLocationImageUrl ??= "https://via.placeholder.com/800x400?text=Harita+Eklenecek";
            createTourDto.MapLocationTitle ??= "Konum Belirtilmedi";
            createTourDto.MapLocationDescription ??= "Bu tur için henüz rota açıklaması girilmedi.";
            createTourDto.CreatedDate = DateTime.Now;

            ModelState.Clear();

            await _tourService.CreateTourAsync(createTourDto);
            return RedirectToAction("TourList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTour(string id)
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            var tour = await _tourService.GetTourByIdAsync(id);

            var model = new UpdateTourDto
            {
                TourId = tour.TourId,
                Title = tour.Title,
                Description = tour.Description,
                Price = tour.Price,
                Capacity = tour.Capacity,
                DayNight = tour.DayNight,
                CoverImageUrl = tour.CoverImageUrl,
                Status = tour.Status,
                CategoryId = tour.CategoryId,
                Badge = tour.Badge,
                MapLocationImageUrl = tour.MapLocationImageUrl,
                MapLocationTitle = tour.MapLocationTitle,
                MapLocationDescription = tour.MapLocationDescription,
                CreatedDate = tour.CreatedDate
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTour(string id, UpdateTourDto updateTourDto)
        {
            updateTourDto.TourId = id;

            var existingTour = await _tourService.GetTourByIdAsync(id);
            if (existingTour != null)
            {
                // DÜZELTİLEN KISIM ( ??= işareti eklendi )
                // Sadece formdan boş gelirse eski veriyi kullan, dolu gelirse formdan geleni (yeni linki) kaydet!
                updateTourDto.MapLocationImageUrl ??= existingTour.MapLocationImageUrl;
                updateTourDto.MapLocationTitle ??= existingTour.MapLocationTitle;
                updateTourDto.MapLocationDescription ??= existingTour.MapLocationDescription;
                updateTourDto.CreatedDate = existingTour.CreatedDate;
            }

            ModelState.Clear();

            await _tourService.UpdateTourAsync(updateTourDto);
            return RedirectToAction("TourList");
        }

        public async Task<IActionResult> DeleteTour(string id)
        {
            await _tourService.DeleteTourAsync(id);
            return RedirectToAction("TourList");
        }
    }
}