using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Dtos.ReservationDtos;
using Case3Vitour.Services.ReservationServices;
using Case3Vitour.Services.TourServices;
using System.Threading.Tasks;

namespace Case3Vitour.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITourService _tourService;

        public ReservationController(IReservationService reservationService, ITourService tourService)
        {
            _reservationService = reservationService;
            _tourService = tourService;
        }

        public async Task<IActionResult> ReservationList()
        {
            // Turları view'a gönderiyoruz ki ID'leri isimlerle eşleştirebilsin
            ViewBag.Tours = await _tourService.GetAllTourAsync();

            var values = await _reservationService.GetAllReservationAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateReservation()
        {
            
            ViewBag.Tours = await _tourService.GetAllTourAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            // Sistem tarafından otomatik doldurulacak alanları doğrulamadan (validation) çıkarıyoruz.
            ModelState.Remove("ReservationCode");
            ModelState.Remove("TotalPrice");
            ModelState.Remove("ReservationDate");

            if (!ModelState.IsValid)
            {
                ViewBag.Tours = await _tourService.GetAllTourAsync();
                return View(createReservationDto);
            }

            try
            {
                await _reservationService.CreateReservationAsync(createReservationDto);
                return RedirectToAction("ReservationList");
            }
            catch (System.Exception ex)
            {
                // Kapasite aşıldığında Service'ten fırlatılan hata buraya düşer 
                // ve kırmızı hata kutusunda ekrana basılır.
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.Tours = await _tourService.GetAllTourAsync();
                return View(createReservationDto);
            }
        }

        public async Task<IActionResult> DeleteReservation(string id)
        {

            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction("ReservationList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateReservation(string id)
        {
            // 1. Turları dropdown (SelectListItem) formatında arayüze gönderiyoruz
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = tours.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = x.Title,
                Value = x.TourId
            }).ToList();

            // 2. Güncellenecek rezervasyonu veritabanından çekiyoruz
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            // 3. Gelen veriyi senin DTO formatına eşliyoruz
            var dto = new UpdateReservationDto
            {
                ReservationId = reservation.ReservationId,
                TourId = reservation.TourId,
                NameSurname = reservation.NameSurname,
                Email = reservation.Email,
                ReservationCode = reservation.ReservationCode,
                PersonCount = reservation.PersonCount,
                TotalPrice = reservation.TotalPrice,
                ReservationDate = reservation.ReservationDate
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            // Eğer formda boş bırakılan zorunlu alan varsa veya hata çıkarsa sayfayı geri döndür
            if (!ModelState.IsValid)
            {
                var tours = await _tourService.GetAllTourAsync();
                ViewBag.Tours = tours.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x.Title,
                    Value = x.TourId
                }).ToList();

                return View(updateReservationDto);
            }

            // Her şey yolundaysa güncelleme işlemini servise gönder ve listeye dön
            await _reservationService.UpdateReservationAsync(updateReservationDto);
            return RedirectToAction("ReservationList");
        }

        // Tura özel rezervasyonları listelemek için
        public async Task<IActionResult> ReservationListByTourId(string tourId)
        {
            var values = await _reservationService.GetReservationsByTourIdAsync(tourId);
            return View(values);
        }
    }
}