using MongoDB.Driver;
using Case3Vitour.Dtos.ReportDtos;
using Case3Vitour.Entities;
using Case3Vitour.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case3Vitour.Services.ReportServices
{
    public class ReportService : IReportService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Review> _reviewCollection;
        private readonly IMongoCollection<Tour> _tourCollection;

        public ReportService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _reservationCollection = database.GetCollection<Reservation>(_databaseSettings.ReservationCollectionName);
            _reviewCollection = database.GetCollection<Review>(_databaseSettings.ReviewCollectionName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
        }

        public async Task<List<KpiCardDto>> GetKpiCardsAsync()
        {
            var now = DateTime.UtcNow;
            // Geçen ay ve bu ayın verilerini karşılaştırıyoruz
            var currentReservations = await _reservationCollection.Find(x => x.ReservationDate.Month == now.Month && x.ReservationDate.Year == now.Year).ToListAsync();
            var prevDate = now.AddMonths(-1);
            var prevReservations = await _reservationCollection.Find(x => x.ReservationDate.Month == prevDate.Month && x.ReservationDate.Year == prevDate.Year).ToListAsync();

            decimal currentRevenue = currentReservations.Sum(x => x.TotalPrice);
            decimal prevRevenue = prevReservations.Sum(x => x.TotalPrice);

            return new List<KpiCardDto>
            {
                new KpiCardDto {
                    Title = "Toplam Gelir",
                    Value = $"₺{currentRevenue:N0}",
                    PreviousValue = $"₺{prevRevenue:N0}",
                    PercentageChange = CalculateChange(currentRevenue, prevRevenue),
                    IsIncrease = currentRevenue >= prevRevenue
                },
                new KpiCardDto {
                    Title = "Toplam Rezervasyon",
                    Value = currentReservations.Count.ToString(),
                    PreviousValue = prevReservations.Count.ToString(),
                    PercentageChange = CalculateChange(currentReservations.Count, prevReservations.Count),
                    IsIncrease = currentReservations.Count >= prevReservations.Count
                }
            };
        }

        public async Task<List<MonthlyGoalDto>> GetMonthlyGoalsAsync()
        {
            var reservations = await _reservationCollection.Find(x => x.ReservationDate.Month == DateTime.UtcNow.Month).ToListAsync();
            var totalRevenue = reservations.Sum(x => x.TotalPrice);

            return new List<MonthlyGoalDto>
            {
                new MonthlyGoalDto { Name="Gelir Hedefi", CurrentValue=totalRevenue, TargetValue=500000M, Unit="₺"},
                new MonthlyGoalDto { Name="Yeni Müşteri", CurrentValue=reservations.Sum(x=>x.PersonCount), TargetValue=200, Unit="kişi" }
            };
        }

        public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenueChartAsync()
        {
            var result = new List<MonthlyRevenueDto>();
            for (int i = 5; i >= 0; i--) // Son 6 ayı getir
            {
                var date = DateTime.UtcNow.AddMonths(-i);
                var revenue = (await _reservationCollection.Find(x => x.ReservationDate.Month == date.Month && x.ReservationDate.Year == date.Year).ToListAsync()).Sum(s => s.TotalPrice);
                result.Add(new MonthlyRevenueDto { Month = date.ToString("MMM"), Revenue = revenue });
            }
            return result;
        }

        public async Task<List<ResultTopTourDto>> GetTopTourListAsync()
        {
            var reservations = await _reservationCollection.Find(x => true).ToListAsync();
            var tours = await _tourCollection.Find(x => true).ToListAsync();

            return reservations.GroupBy(r => r.TourId)
                .Select(g => new ResultTopTourDto
                {
                    TourId = g.Key,
                    Title = tours.FirstOrDefault(t => t.TourId == g.Key)?.Title ?? "Bilinmiyor",
                    TotalReservations = g.Count()
                }).OrderByDescending(x => x.TotalReservations).Take(5).ToList();
        }

        private double CalculateChange(decimal current, decimal prev)
        {
            if (prev == 0) return current > 0 ? 100 : 0;
            return (double)((current - prev) / prev) * 100;
        }
    }
}