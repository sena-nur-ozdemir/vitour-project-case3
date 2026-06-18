using AutoMapper;
using Case3Vitour.Dtos.GalleryDtos;
using Case3Vitour.Entities;
using Case3Vitour.Settings;
using MongoDB.Driver;

namespace Case3Vitour.Services.GalleryServices
{
    public class GalleryService : IGalleryService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Gallery> _galleryCollection;

        public GalleryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _galleryCollection = database.GetCollection<Gallery>(databaseSettings.GalleryCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultGalleryDto>> GetAllImagesAsync()
        {
            var values = await _galleryCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultGalleryDto>>(values);
        }

        public async Task<List<ResultGalleryByTourIdDto>> GetAllGalleryByTourIdAsync(string id)
        {
            var values = await _galleryCollection.Find(x => x.TourId == id).ToListAsync();
            return _mapper.Map<List<ResultGalleryByTourIdDto>>(values);
        }

        public async Task<GetGalleryByIdDto> GetImageByIdAsync(string id)
        {
            var value = await _galleryCollection.Find(x => x.GalleryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetGalleryByIdDto>(value);
        }

        public async Task CreateImageAsync(CreateGalleryDto createGalleryDto)
        {
            var value = _mapper.Map<Gallery>(createGalleryDto);
            await _galleryCollection.InsertOneAsync(value);
        }

        public async Task UpdateImageAsync(UpdateGalleryDto updateGalleryDto)
        {
            var value = _mapper.Map<Gallery>(updateGalleryDto);
            await _galleryCollection.FindOneAndReplaceAsync(x => x.GalleryId == updateGalleryDto.GalleryId, value);
        }

        public async Task DeleteImageAsync(string id)
        {
            await _galleryCollection.DeleteOneAsync(x => x.GalleryId == id);
        }

    }
}