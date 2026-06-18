using AutoMapper;
using Case3Vitour.Dtos.TourDtos;
using Case3Vitour.Entities;
using Case3Vitour.Settings;
using MongoDB.Driver;

namespace Case3Vitour.Services.TourServices
{
    public class TourService : ITourService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Tour> _tourCollection;
        private readonly IMongoCollection<Review> _reviewCollection;
        private readonly IMongoCollection<Gallery> _galleryCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public TourService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _tourCollection = database.GetCollection<Tour>(databaseSettings.TourCollectionName);
            _reviewCollection = database.GetCollection<Review>(databaseSettings.ReviewCollectionName);
            _galleryCollection = database.GetCollection<Gallery>(databaseSettings.GalleryCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultTourDto>> GetAllTourAsync()
        {
            var values = await _tourCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(values);
        }

        public async Task<List<ResultTourDto>> GetAllTourWithCategoryAsync()
        {
            var tours = await _tourCollection.Find(x => true).ToListAsync();
            var categories = await _categoryCollection.Find(x => true).ToListAsync();
            var mappedTours = _mapper.Map<List<ResultTourDto>>(tours);

            foreach (var tour in mappedTours)
            {
                tour.CategoryName = categories.FirstOrDefault(x => x.CategoryId == tour.CategoryId)?.CategoryName;
            }
            return mappedTours;
        }

        public async Task<GetTourByIdDto> GetTourByIdAsync(string id)
        {
            var value = await _tourCollection.Find(x => x.TourId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetTourByIdDto>(value);
        }

        public async Task<List<TourCardDto>> GetTourCardListAsync()
        {
            var tours = await _tourCollection.Find(x => true).ToListAsync();
            var tourDtos = _mapper.Map<List<TourCardDto>>(tours);

            foreach (var tour in tourDtos)
            {
                var reviews = await _reviewCollection.Find(x => x.TourId == tour.TourId).ToListAsync();
                var galleries = await _galleryCollection.Find(x => x.TourId == tour.TourId).ToListAsync();
                var category = await _categoryCollection.Find(x => x.CategoryId == tour.CategoryId).FirstOrDefaultAsync();

                tour.CategoryName = category?.CategoryName;
                tour.GalleryCount = galleries.Count;
                tour.ReviewCount = reviews.Count;

                // Hata alınan kısım: ComfortScore yerine ValueForMoneyScore kullandık
                tour.ReviewScore = reviews.Count == 0 ? 0 :
                    Math.Round(reviews.Average(x => (x.GuideScore + x.AccommodationScore + x.TransportationScore + x.ValueForMoneyScore) / 4.0), 1);
            }
            return tourDtos;
        }

        public async Task<TourDetailDto> GetTourDetailAsync(string id)
        {
            var tour = await _tourCollection.Find(x => x.TourId == id).FirstOrDefaultAsync();
            if (tour == null) return null!;

            var tourDetailDto = _mapper.Map<TourDetailDto>(tour);
            var reviews = await _reviewCollection.Find(x => x.TourId == id).ToListAsync();
            var category = await _categoryCollection.Find(x => x.CategoryId == tour.CategoryId).FirstOrDefaultAsync();

            tourDetailDto.CategoryName = category?.CategoryName;
            tourDetailDto.ReviewCount = reviews.Count;

            // Hata alınan kısım: ComfortScore yerine ValueForMoneyScore kullandık
            tourDetailDto.ReviewScore = reviews.Count == 0 ? 0 :
                Math.Round(reviews.Average(x => (x.GuideScore + x.AccommodationScore + x.TransportationScore + x.ValueForMoneyScore) / 4.0), 1);

            return tourDetailDto;
        }

        public async Task<(List<TourCardDto> Tours, int TotalCount)> GetPagedToursAsync(int page, int pageSize)
        {
            var totalCount = (int)await _tourCollection.CountDocumentsAsync(x => true);
            var tours = await _tourCollection.Find(x => true).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();
            var tourDtos = _mapper.Map<List<TourCardDto>>(tours);

            foreach (var tour in tourDtos)
            {
                var reviews = await _reviewCollection.Find(x => x.TourId == tour.TourId).ToListAsync();
                var category = await _categoryCollection.Find(x => x.CategoryId == tour.CategoryId).FirstOrDefaultAsync();

                // EKSİK OLAN KISIM EKLENDİ: Galeri Sayısı Çekiliyor
                var galleries = await _galleryCollection.Find(x => x.TourId == tour.TourId).ToListAsync();

                tour.CategoryName = category?.CategoryName;
                tour.ReviewCount = reviews.Count;

                // EKSİK OLAN KISIM EKLENDİ: Galeri Sayısı DTO'ya atanıyor
                tour.GalleryCount = galleries.Count;

                tour.ReviewScore = reviews.Count == 0 ? 0 :
                    Math.Round(reviews.Average(x => (x.GuideScore + x.AccommodationScore + x.TransportationScore + x.ValueForMoneyScore) / 4.0), 1);
            }
            return (tourDtos, totalCount);
        }

        public async Task<List<ResultTourDto>> GetLast3TourAsync()
        {
            var values = await _tourCollection.Find(x => true).SortByDescending(x => x.CreatedDate).Limit(3).ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(values);
        }

        public async Task CreateTourAsync(CreateTourDto createTourDto)
        {
            var value = _mapper.Map<Tour>(createTourDto);
            await _tourCollection.InsertOneAsync(value);
        }

        public async Task UpdateTourAsync(UpdateTourDto updateTourDto)
        {
            var value = _mapper.Map<Tour>(updateTourDto);
            value.TourId = updateTourDto.TourId;
            await _tourCollection.FindOneAndReplaceAsync(x => x.TourId == updateTourDto.TourId, value);
        }

        public async Task DeleteTourAsync(string id)
        {
            await _tourCollection.DeleteOneAsync(x => x.TourId == id);
        }

        public async Task<(List<TourCardDto> Tours, int TotalCount)> GetPagedToursAsync(int page, int pageSize, string categoryId = null)
        {
            // 1. FİLTRE MANTIĞI BURADA ÇALIŞIYOR
            var filter = Builders<Tour>.Filter.Empty;
            if (!string.IsNullOrEmpty(categoryId))
            {
                filter = Builders<Tour>.Filter.Eq(x => x.CategoryId, categoryId);
            }

            var totalCount = (int)await _tourCollection.CountDocumentsAsync(filter);
            var tours = await _tourCollection.Find(filter).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();
            var tourDtos = _mapper.Map<List<TourCardDto>>(tours);

            foreach (var tour in tourDtos)
            {
                var reviews = await _reviewCollection.Find(x => x.TourId == tour.TourId).ToListAsync();
                var category = await _categoryCollection.Find(x => x.CategoryId == tour.CategoryId).FirstOrDefaultAsync();
                var galleries = await _galleryCollection.Find(x => x.TourId == tour.TourId).ToListAsync();

                tour.CategoryName = category?.CategoryName;
                tour.GalleryCount = galleries.Count;

                // 2. YILDIZ MANTIĞI: Sadece içinde gerçekten puan olan geçerli yorumları alıyoruz
                var validReviews = reviews.Where(x => x.ValueForMoneyScore > 0 || x.GuideScore > 0).ToList();

                tour.ReviewCount = validReviews.Count;

                // 0.0 Hatasının çözümü: Sadece geçerli yorumların ortalamasını al
                tour.ReviewScore = validReviews.Count == 0 ? 0 :
                    Math.Round(validReviews.Average(x => (x.GuideScore + x.AccommodationScore + x.TransportationScore + x.ValueForMoneyScore) / 4.0), 1);
            }

            return (tourDtos, totalCount);
        }


    }
}
