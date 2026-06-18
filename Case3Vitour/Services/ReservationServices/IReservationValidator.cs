using System.Threading.Tasks;

namespace Case3Vitour.Services.ReservationServices
{
    public interface IReservationValidator
    {
        Task CheckCapacityAsync(string tourId, int requestedPersonCount);
    }
}