using System.Collections.Generic;
using System.Threading.Tasks;
using Case3Vitour.Dtos.TourPlanDtos;

namespace Case3Vitour.Services.TourPlanServices
{
    public interface ITourPlanService
    {
        Task<List<ResultTourPlanDto>> GetAllTourPlanAsync();
        Task<List<ResultTourPlanByTourIdDto>> GetAllTourPlanByTourIdAsync(string id);
        Task<GetTourPlanByIdDto> GetTourPlanByIdAsync(string id);
        Task CreateTourPlanAsync(CreateTourPlanDto createTourPlanDto);
        Task UpdateTourPlanAsync(UpdateTourPlanDto updateTourPlanDto);
        Task DeleteTourPlanAsync(string id);
    }
}