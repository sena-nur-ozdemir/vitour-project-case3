using Microsoft.AspNetCore.Mvc;

namespace Case3Vitour.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsAddReviewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(string id)
        {
            ViewBag.TourId = id;
            return View();
        }
    }
}