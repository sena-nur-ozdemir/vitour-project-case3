using MongoDB.Driver;
using Case3Vitour.Entities;
using Case3Vitour.Settings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Case3Vitour.Services.ReservationServices
{
    public class ReservationValidator : IReservationValidator
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Tour> _tourCollection;

        public ReservationValidator(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _reservationCollection = database.GetCollection<Reservation>(databaseSettings.ReservationCollectionName);
            _tourCollection = database.GetCollection<Tour>(databaseSettings.TourCollectionName);
        }

        public async Task CheckCapacityAsync(string tourId, int requestedPersonCount)
        {
            var tour = await _tourCollection.Find(x => x.TourId == tourId).FirstOrDefaultAsync();
            if (tour == null) throw new Exception("Tur bulunamadı!");

            var reservations = await _reservationCollection.Find(x => x.TourId == tourId).ToListAsync();
            var totalReserved = reservations.Sum(x => x.PersonCount);

            if (totalReserved + requestedPersonCount > tour.Capacity)
            {
                throw new Exception("Üzgünüz, bu tur için yeterli kontenjan kalmamıştır.");
            }
        }
    }
}