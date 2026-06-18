using AutoMapper;
using Case3Vitour.Dtos.CategoryDtos;
using Case3Vitour.Entities;
using Case3Vitour.Settings;
using MongoDB.Driver;

namespace Case3Vitour.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Tour> _tourCollection;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _tourCollection = database.GetCollection<Tour>(databaseSettings.TourCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var categories = await _categoryCollection.Find(x => true).ToListAsync();
            var tours = await _tourCollection.Find(x => true).ToListAsync();

            // Kategorilere göre turları gruplayıp sayılarını alıyoruz
            var tourCounts = tours.GroupBy(x => x.CategoryId).ToDictionary(g => g.Key, g => g.Count());

            var mappedCategories = _mapper.Map<List<ResultCategoryDto>>(categories);

            foreach (var category in mappedCategories)
            {
                category.TourCount = tourCounts.ContainsKey(category.CategoryId) ? tourCounts[category.CategoryId] : 0;
            }
            return mappedCategories;
        }

        public async Task<List<ResultCategoryDto>> GetActiveCategoriesAsync()
        {
            var values = await _categoryCollection.Find(x => x.CategoryStatus == true).ToListAsync();
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id)
        {
            var value = await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetCategoryByIdDto>(value);
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);
            await _categoryCollection.InsertOneAsync(value);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var value = _mapper.Map<Category>(updateCategoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == updateCategoryDto.CategoryId, value);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id);
        }

        public async Task BulkDeleteAsync(List<string> ids)
        {
            var filter = Builders<Category>.Filter.In(x => x.CategoryId, ids);
            await _categoryCollection.DeleteManyAsync(filter);
        }
    }
}