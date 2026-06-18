using AutoMapper;
using Case3Vitour.Dtos.TourPlanDtos;
using Case3Vitour.Entities;
using Case3Vitour.Settings;
using MongoDB.Driver;

namespace Case3Vitour.Services.TourPlanServices
{
    public class TourPlanService : ITourPlanService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<TourPlan> _tourPlanCollection;

        public TourPlanService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _tourPlanCollection = database.GetCollection<TourPlan>(databaseSettings.TourPlanCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultTourPlanDto>> GetAllTourPlanAsync()
        {
            var values = await _tourPlanCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultTourPlanDto>>(values);
        }

        public async Task<List<ResultTourPlanByTourIdDto>> GetAllTourPlanByTourIdAsync(string id)
        {
            
            var values = await _tourPlanCollection.Find(x => x.TourId == id).ToListAsync();
            return _mapper.Map<List<ResultTourPlanByTourIdDto>>(values);
        }

        public async Task<GetTourPlanByIdDto> GetTourPlanByIdAsync(string id)
        {
            var value = await _tourPlanCollection.Find(x => x.TourPlanId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetTourPlanByIdDto>(value);
        }

        public async Task CreateTourPlanAsync(CreateTourPlanDto createTourPlanDto)
        {
            var value = _mapper.Map<TourPlan>(createTourPlanDto);
            await _tourPlanCollection.InsertOneAsync(value);
        }

        public async Task UpdateTourPlanAsync(UpdateTourPlanDto updateTourPlanDto)
        {
            var value = _mapper.Map<TourPlan>(updateTourPlanDto);
            await _tourPlanCollection.FindOneAndReplaceAsync(x => x.TourPlanId == updateTourPlanDto.TourPlanId, value);
        }

        public async Task DeleteTourPlanAsync(string id)
        {
            await _tourPlanCollection.DeleteOneAsync(x => x.TourPlanId == id);
        }
    }
}