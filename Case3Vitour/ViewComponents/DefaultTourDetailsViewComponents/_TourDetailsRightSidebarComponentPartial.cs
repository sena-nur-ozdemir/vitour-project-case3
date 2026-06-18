using Microsoft.AspNetCore.Mvc;

namespace Case3Vitour.ViewComponents.DefaultViewComponents
{
    public class _TourDetailsRightSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(string id)
        {
            // Tur ID'sini View'a taşıyoruz ki rezervasyon butonuna ekleyebilelim
            ViewBag.TourId = id;
            return View();
        }
    }
}