using Case3Vitour.Dtos.GalleryDtos;

namespace Case3Vitour.Services.GalleryServices
{
    public interface IGalleryService
    {
        Task<List<ResultGalleryDto>> GetAllImagesAsync();

        Task<List<ResultGalleryByTourIdDto>> GetAllGalleryByTourIdAsync(string id);

        Task<GetGalleryByIdDto> GetImageByIdAsync(string id);
        Task CreateImageAsync(CreateGalleryDto createGalleryDto);
        Task UpdateImageAsync(UpdateGalleryDto updateGalleryDto);
        Task DeleteImageAsync(string id);
    }
}