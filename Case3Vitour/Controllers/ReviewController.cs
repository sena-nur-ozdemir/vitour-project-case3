using Case3Vitour.Dtos.ReviewDtos;
using Case3Vitour.Services.ReviewServices;
using Case3Vitour.Services.TourServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Case3Vitour.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ITourService _tourService;

        public ReviewController(IReviewService reviewService, ITourService tourService)
        {
            _reviewService = reviewService;
            _tourService = tourService;
        }

        public async Task<IActionResult> ReviewList()
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = tours.Select(t => new SelectListItem
            {
                Value = t.TourId,
                Text = t.Title
            }).ToList();

            var values = await _reviewService.GetAllReviewAsync();
            return View(values);
        }

        // Tura göre yorumları filtreleme
        public async Task<IActionResult> ReviewListByTourId(string id)
        {
            ViewBag.Tour = await _tourService.GetTourByIdAsync(id);
            var values = await _reviewService.GetAllReviewsByTourIdAsync(id);
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateReview(string? tourId = null)
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = tours.Select(t => new SelectListItem
            {
                Value = t.TourId,
                Text = t.Title
            }).ToList();

            var dto = new CreateReviewDto();
            if (!string.IsNullOrEmpty(tourId))
            {
                dto.TourId = tourId;
            }

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview(CreateReviewDto createReviewDto)
        {
            if (!ModelState.IsValid)
            {
                var tours = await _tourService.GetAllTourAsync();
                ViewBag.Tours = tours.Select(t => new SelectListItem { Value = t.TourId, Text = t.Title }).ToList();
                return View(createReviewDto);
            }

            // Yorumun yapıldığı tarihi otomatik olarak şu anki zaman yapıyoruz
            createReviewDto.ReviewDate = DateTime.Now;

            // 4 farklı kriterin ortalamasını alıp ana Score alanına atıyoruz
            createReviewDto.Score = (createReviewDto.GuideScore + createReviewDto.AccommodationScore + createReviewDto.TransportationScore + createReviewDto.ValueForMoneyScore) / 4.0;

            await _reviewService.CreateReviewAsync(createReviewDto);

            return RedirectToAction("TourDetails", "DefaultTour", new { id = createReviewDto.TourId, submittedReview = true });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(string id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return RedirectToAction("ReviewList");
        }

        // --- TEKLİ ONAYLAMA (Tablodaki onay butonu için) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveReview(string id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);

            if (review != null)
            {
                // Sadece UpdateReviewDto içinde var olan alanları eşliyoruz
                var updateReviewDto = new UpdateReviewDto
                {
                    ReviewId = review.ReviewId,
                    TourId = review.TourId,
                    NameSurname = review.NameSurname,
                    Comment = review.Comment,
                    Score = review.Score,
                    ReviewDate = review.ReviewDate,
                    Status = true // Onaylandığı için yayına alıyoruz
                };

                await _reviewService.UpdateReviewAsync(updateReviewDto);
            }
            return RedirectToAction("ReviewList");
        }

        // --- TOPLU SİLME (Yeşil bar üzerindeki buton için) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkDelete(string[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    await _reviewService.DeleteReviewAsync(id);
                }
            }
            return RedirectToAction("ReviewList");
        }

        // --- TOPLU ONAYLAMA (Yeşil bar üzerindeki buton için) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkApprove(string[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    var review = await _reviewService.GetReviewByIdAsync(id);
                    if (review != null)
                    {
                        //Sadece UpdateReviewDto içinde var olan alanları eşliyoruz
                        var updateReviewDto = new UpdateReviewDto
                        {
                            ReviewId = review.ReviewId,
                            TourId = review.TourId,
                            NameSurname = review.NameSurname,
                            Comment = review.Comment,
                            Score = review.Score,
                            ReviewDate = review.ReviewDate,
                            Status = true
                        };
                        await _reviewService.UpdateReviewAsync(updateReviewDto);
                    }
                }
            }
            return RedirectToAction("ReviewList");
        }

    }
}