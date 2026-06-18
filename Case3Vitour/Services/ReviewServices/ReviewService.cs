using AutoMapper;
using Case3Vitour.Dtos.ReviewDtos;
using Case3Vitour.Entities;
using Case3Vitour.Settings;
using MongoDB.Driver;

namespace Case3Vitour.Services.ReviewServices
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Review> _reviewCollection;

        public ReviewService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _reviewCollection = database.GetCollection<Review>(databaseSettings.ReviewCollectionName);
            _mapper = mapper;
        }

        public async Task CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            var value = _mapper.Map<Review>(createReviewDto);

            // 4 farklı puanın ortalamasını otomatik hesaplıyoruz
            value.Score = (value.GuideScore + value.TransportationScore + value.AccommodationScore + value.ValueForMoneyScore) / 4.0;
            value.ReviewDate = DateTime.Now;
            value.Status = true;

            await _reviewCollection.InsertOneAsync(value);
        }

        public async Task<List<ResultReviewDto>> GetAllReviewAsync()
        {
            var values = await _reviewCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultReviewDto>>(values);
        }

        public async Task<List<ResultReviewByTourIdDto>> GetAllReviewsByTourIdAsync(string id)
        {
            var values = await _reviewCollection.Find(x => x.TourId == id).ToListAsync();
            return _mapper.Map<List<ResultReviewByTourIdDto>>(values);
        }

        public async Task<ReviewSummaryDto> GetReviewSummaryByTourIdAsync(string tourId)
        {
            var values = await _reviewCollection.Find(x => x.TourId == tourId).ToListAsync();

            if (!values.Any()) return new ReviewSummaryDto();

            return new ReviewSummaryDto
            {
                AverageScore = values.Average(x => x.Score),
                GuideScore = values.Average(x => x.GuideScore),
                TransportationScore = values.Average(x => x.TransportationScore),
                AccommodationScore = values.Average(x => x.AccommodationScore),
                ValueForMoneyScore = values.Average(x => x.ValueForMoneyScore)
            };
        }

        public async Task<GetReviewByIdDto> GetReviewByIdAsync(string id)
        {
            var value = await _reviewCollection.Find(x => x.ReviewId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetReviewByIdDto>(value);
        }

        public async Task UpdateReviewAsync(UpdateReviewDto updateReviewDto)
        {
            var value = _mapper.Map<Review>(updateReviewDto);
            await _reviewCollection.FindOneAndReplaceAsync(x => x.ReviewId == updateReviewDto.ReviewId, value);
        }

        public async Task DeleteReviewAsync(string id)
        {
            await _reviewCollection.DeleteOneAsync(x => x.ReviewId == id);
        }
    }
}