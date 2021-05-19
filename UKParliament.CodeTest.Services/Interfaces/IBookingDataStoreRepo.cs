using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IBookingDataStoreRepo
    {
        Task<RoomBookingRepoActionResult> AddBookingAsync(RoomBooking roomBooking);

        Task<RoomBookingRepoActionResult> DeleteBookingAsync(RoomBooking roomBooking);

        Task<RoomBookingRepoActionResult> FindAvailableRoomsAsync(RoomBookingPeriod roomBookingSearch);
    }
}
