using Case3Vitour.Dtos.TourDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case3Vitour.Services.TourServices
{
    public interface ITourService
    {
        Task<List<ResultTourDto>> GetAllTourAsync();
        Task<List<ResultTourDto>> GetAllTourWithCategoryAsync();
        Task<GetTourByIdDto> GetTourByIdAsync(string id);
        Task<List<TourCardDto>> GetTourCardListAsync();
        Task<TourDetailDto> GetTourDetailAsync(string id);
        Task<(List<TourCardDto> Tours, int TotalCount)> GetPagedToursAsync(int page, int pageSize, string categoryId = null);
        Task<List<ResultTourDto>> GetLast3TourAsync();
        Task CreateTourAsync(CreateTourDto createTourDto);
        Task UpdateTourAsync(UpdateTourDto updateTourDto);
        Task DeleteTourAsync(string id);
    }
}