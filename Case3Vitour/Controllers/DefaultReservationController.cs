using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Dtos.ReservationDtos;
using Case3Vitour.Services.ReservationServices;
using Case3Vitour.Services.TourServices;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Case3Vitour.Controllers
{
    public class DefaultReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITourService _tourService;

        public DefaultReservationController(IReservationService reservationService, ITourService tourService)
        {
            _reservationService = reservationService;
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateReservation(string id)
        {
            // GetAllTourWithCategoryAsync kullanarak kategori ve diğer detayları da çekiyoruz
            var tours = await _tourService.GetAllTourWithCategoryAsync();
            ViewBag.Tours = tours;

            // Eğer ID geldiyse, View'daki sağ panelin dolması için seçili turu gönderiyoruz
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.SelectedTourId = id;
                ViewBag.SelectedTour = tours.FirstOrDefault(x => x.TourId == id);
            }

            return View(new CreateReservationDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            try
            {
                var code = await _reservationService.CreateReservationAsync(createReservationDto);
                return RedirectToAction("ReservationConfirm", new { code });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewBag.Tours = await _tourService.GetAllTourWithCategoryAsync();
                return View(createReservationDto);
            }
        }

        public async Task<IActionResult> ReservationConfirm(string code)
        {
            var reservation = await _reservationService.GetReservationByCodeAsync(code);

            if (reservation == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(reservation);
        }
    }
}