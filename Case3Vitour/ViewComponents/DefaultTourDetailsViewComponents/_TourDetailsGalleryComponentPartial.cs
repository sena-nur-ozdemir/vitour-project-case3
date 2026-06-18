using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Services.GalleryServices;

namespace Case3Vitour.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsGalleryComponentPartial : ViewComponent
    {
        private readonly IGalleryService _galleryService;

        public _TourDetailsGalleryComponentPartial(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values = await _galleryService.GetAllGalleryByTourIdAsync(id);
            return View(values);
        }
    }
}