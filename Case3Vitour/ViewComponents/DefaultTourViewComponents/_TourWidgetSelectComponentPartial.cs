using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Services.CategoryServices;
using AutoMapper; 
using Case3Vitour.Dtos.CategoryDtos;

namespace Case3Vitour.ViewComponents.DefaultTourViewComponents
{
    public class _TourWidgetSelectComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public _TourWidgetSelectComponentPartial(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Veritabanından tüm kategorileri çekiyoruz (Entity formatında)
            var categories = await _categoryService.GetAllCategoryAsync();

            // 2. Ham veriyi (Entity), View'ın beklediği formata (DTO) dönüştürüyoruz
            var values = _mapper.Map<List<ResultCategoryDto>>(categories);

            // 3. Dönüştürülmüş listeyi View'a gönderiyoruz
            return View(values);
        }
    }
}