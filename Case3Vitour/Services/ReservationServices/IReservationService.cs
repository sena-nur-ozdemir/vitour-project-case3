using Case3Vitour.Dtos.ReservationDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case3Vitour.Services.ReservationServices
{
    public interface IReservationService
    {
        // Tüm rezervasyonları listeleme (Admin)
        Task<List<ResultReservationDto>> GetAllReservationAsync();

        // Belirli bir tura ait rezervasyonları listeleme
        Task<List<ResultReservationByTourIdDto>> GetReservationsByTourIdAsync(string id);

        // Rezervasyon detayını getirme
        Task<GetReservationByIdDto> GetReservationByIdAsync(string id);

        // Rezervasyon koduna (Örn: A7B8C9D0) göre getirme
        Task<GetReservationByIdDto> GetReservationByCodeAsync(string code);

        // Yeni rezervasyon oluşturma (Geriye oluşturulan kod döner)
        Task<string> CreateReservationAsync(CreateReservationDto createReservationDto);

        // Rezervasyon güncelleme
        Task UpdateReservationAsync(UpdateReservationDto updateReservationDto);

        // Rezervasyon silme
        Task DeleteReservationAsync(string id);
    }
}