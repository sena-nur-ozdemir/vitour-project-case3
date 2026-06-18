using AutoMapper;
using Case3Vitour.Dtos.ReservationDtos;
using Case3Vitour.Entities;
using Case3Vitour.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case3Vitour.Services.ReservationServices
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Tour> _tourCollection;
        private readonly IMapper _mapper;
        private readonly IReservationValidator _reservationValidator;

        public ReservationService(IMapper mapper, IDatabaseSettings databaseSettings, IReservationValidator reservationValidator)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _reservationCollection = database.GetCollection<Reservation>(databaseSettings.ReservationCollectionName);
            _tourCollection = database.GetCollection<Tour>(databaseSettings.TourCollectionName);

            _mapper = mapper;
            _reservationValidator = reservationValidator;
        }

        public async Task<string> CreateReservationAsync(CreateReservationDto createReservationDto)
        {
            // 1. Kapasite Kontrolü (Validator üzerinden profesyonel kontrol)
            await _reservationValidator.CheckCapacityAsync(createReservationDto.TourId, createReservationDto.PersonCount);

            // 2. Tur bilgilerini alıp fiyat hesaplama
            var tour = await _tourCollection.Find(x => x.TourId == createReservationDto.TourId).FirstOrDefaultAsync();
            if (tour == null) throw new Exception("İlgili tur bulunamadı.");

            // 3. Otomatik Alanları Doldurma
            createReservationDto.TotalPrice = tour.Price * createReservationDto.PersonCount;
            createReservationDto.ReservationDate = DateTime.Now;
            // 8 haneli benzersiz kod üretimi
            createReservationDto.ReservationCode = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            // 4. Map'leme ve Kayıt
            var value = _mapper.Map<Reservation>(createReservationDto);
            
            value.CreatedDate = DateTime.Now;
            value.Status = "Onay Bekliyor";

            await _reservationCollection.InsertOneAsync(value);

            return createReservationDto.ReservationCode;
        }

        public async Task<List<ResultReservationDto>> GetAllReservationAsync()
        {
            var values = await _reservationCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultReservationDto>>(values);
        }

        public async Task<GetReservationByIdDto> GetReservationByIdAsync(string id)
        {
            var value = await _reservationCollection.Find(x => x.ReservationId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetReservationByIdDto>(value);
        }

        public async Task<List<ResultReservationByTourIdDto>> GetReservationsByTourIdAsync(string tourId)
        {
            var values = await _reservationCollection.Find(x => x.TourId == tourId).ToListAsync();
            return _mapper.Map<List<ResultReservationByTourIdDto>>(values);
        }

        public async Task UpdateReservationAsync(UpdateReservationDto updateReservationDto)
        {
            var value = _mapper.Map<Reservation>(updateReservationDto);
            await _reservationCollection.FindOneAndReplaceAsync(x => x.ReservationId == updateReservationDto.ReservationId, value);
        }

        public async Task DeleteReservationAsync(string id)
        {
            await _reservationCollection.DeleteOneAsync(x => x.ReservationId == id);
        }
        public async Task<GetReservationByIdDto> GetReservationByCodeAsync(string code)
        {
            var value = await _reservationCollection.Find(x => x.ReservationCode == code).FirstOrDefaultAsync();
            return _mapper.Map<GetReservationByIdDto>(value);
        }
    }
}