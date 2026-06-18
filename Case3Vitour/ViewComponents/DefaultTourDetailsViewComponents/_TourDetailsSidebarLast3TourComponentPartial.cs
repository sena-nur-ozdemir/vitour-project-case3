using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Services.TourServices;
using AutoMapper;
using Case3Vitour.Dtos.TourDtos;

namespace Case3Vitour.ViewComponents.DefaultViewComponents
{
    public class _TourDetailsSidebarLast3TourComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;
        private readonly IMapper _mapper;

        public _TourDetailsSidebarLast3TourComponentPartial(ITourService tourService, IMapper mapper)
        {
            _tourService = tourService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Veritabanından son 3 turu getir (Bu metodu TourService'e eklemiştik)
            var tours = await _tourService.GetLast3TourAsync();

            // 2. Senin istediğin DTO formatına (ResultTourDto) çevir
            var values = _mapper.Map<List<ResultTourDto>>(tours);

            return View(values);
        }
    }
}