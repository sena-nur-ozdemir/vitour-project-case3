using Case3Vitour.Dtos.ReviewDtos;

namespace Case3Vitour.Services.ReviewServices
{
    public interface IReviewService
    {
        Task<List<ResultReviewDto>> GetAllReviewAsync();
        Task<List<ResultReviewByTourIdDto>> GetAllReviewsByTourIdAsync(string id);
        Task<GetReviewByIdDto> GetReviewByIdAsync(string id);
        Task<ReviewSummaryDto> GetReviewSummaryByTourIdAsync(string tourId); // Tur detayındaki o meşhur özet
        Task CreateReviewAsync(CreateReviewDto createReviewDto);
        Task UpdateReviewAsync(UpdateReviewDto updateReviewDto);
        Task DeleteReviewAsync(string id);
    }
}