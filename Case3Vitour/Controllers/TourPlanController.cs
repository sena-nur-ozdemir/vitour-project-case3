using Case3Vitour.Dtos.TourPlanDtos;
using Case3Vitour.Services.TourPlanServices;
using Case3Vitour.Services.TourServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Case3Vitour.Controllers
{
    public class TourPlanController : Controller
    {
        private readonly ITourPlanService _tourPlanService;
        private readonly ITourService _tourService;

        public TourPlanController(ITourPlanService tourPlanService, ITourService tourService)
        {
            _tourPlanService = tourPlanService;
            _tourService = tourService;
        }

        public async Task<IActionResult> TourPlanList(string id)
        {
            var values = await _tourPlanService.GetAllTourPlanByTourIdAsync(id);
            var tour = await _tourService.GetTourByIdAsync(id);

            ViewBag.TourName = tour?.Title ?? "Tur";
            ViewBag.TourId = id;
            ViewBag.TourDayCount = tour?.DayNight ?? "Belirtilmemiş";
            ViewBag.TourPrice = tour?.Price ?? 0;
            ViewBag.TourCoverImage = tour?.CoverImageUrl;

            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateTourPlan(string id)
        {
            // 1. Dropdown için tüm turları çek
            var tours = await _tourService.GetAllTourAsync();

            // 2. SelectList ile View'a gönder (id gönderirsen, o tur otomatik seçili gelir)
            ViewBag.Tours = new SelectList(tours, "TourId", "Title", id);

            // 3. Ekstra bilgiler
            ViewBag.TourId = id;
            ViewBag.ExistingPlans = await _tourPlanService.GetAllTourPlanByTourIdAsync(id);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTourPlan(CreateTourPlanDto createTourPlanDto)
        {
            // Modele uygun değilse (hata varsa) formu tekrar göster
            if (!ModelState.IsValid)
            {
                var tours = await _tourService.GetAllTourAsync();
                ViewBag.Tours = new SelectList(tours, "TourId", "Title", createTourPlanDto.TourId);
                return View(createTourPlanDto);
            }

            await _tourPlanService.CreateTourPlanAsync(createTourPlanDto);
            return RedirectToAction("TourPlanList", new { id = createTourPlanDto.TourId });
        }

        public async Task<IActionResult> AllTourPlan()
        {
            // 1. Tüm turları çekiyoruz
            var tours = await _tourService.GetAllTourAsync();

            // 2. Tüm planları çekiyoruz
            var allPlans = await _tourPlanService.GetAllTourPlanAsync();

            // 3. ViewBag ile turları View'a taşıyoruz
            ViewBag.Tours = tours;

            return View(allPlans);
        }

        // 1. DÜZENLEME EKRANI (GET)
        [HttpGet]
        public async Task<IActionResult> UpdateTourPlan(string id)
        {
            // Veritabanından mevcut planı çekiyoruz
            var plan = await _tourPlanService.GetTourPlanByIdAsync(id);
            if (plan == null) return NotFound();

            // Dropdown (Tur seçimi) için tüm turları gönderiyoruz
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = new SelectList(tours, "TourId", "Title", plan.TourId);

            // Düzenleme sayfası için ihtiyacın olan diğer bilgiler
            ViewBag.TourId = plan.TourId;

            return View(plan);
        }

        // 2. DÜZENLEME KAYIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTourPlan(UpdateTourPlanDto updateTourPlanDto)
        {
            if (!ModelState.IsValid)
            {
                // Hata varsa turları tekrar gönder ki liste boşalmasın
                var tours = await _tourService.GetAllTourAsync();
                ViewBag.Tours = new SelectList(tours, "TourId", "Title", updateTourPlanDto.TourId);
                return View(updateTourPlanDto);
            }

            await _tourPlanService.UpdateTourPlanAsync(updateTourPlanDto);

            // İşlem bitince o turun plan listesine geri dön
            return RedirectToAction("TourPlanList", new { id = updateTourPlanDto.TourId });
        }
        
        public async Task<IActionResult> DeleteTourPlan(string id, string tourId)
        {
            await _tourPlanService.DeleteTourPlanAsync(id);

            // İşlem yapılan (tıklanan) önceki sayfanın URL'sini al
            string refererUrl = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(refererUrl))
            {
                return Redirect(refererUrl); // Geldiğin sayfaya geri dön
            }

            // Eğer tarayıcı kaynaklı referer bulunamazsa yedek plan olarak tüm planlara dön
            return RedirectToAction("AllTourPlan");
        }
    }
}