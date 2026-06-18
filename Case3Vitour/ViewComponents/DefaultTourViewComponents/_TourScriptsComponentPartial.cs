using Microsoft.AspNetCore.Mvc;

namespace Case3Vitour.ViewComponents.DefaultTourViewComponents
{
    public class _TourScriptsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}