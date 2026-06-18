using Case3Vitour.Dtos.CategoryDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case3Vitour.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task<List<ResultCategoryDto>> GetActiveCategoriesAsync();
        Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id);
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(string id);
        Task BulkDeleteAsync(List<string> ids); 
    }
}