using System.Collections.Generic;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IBookingsService
    {
        Task<RoomBooking> AddRoomBookingAsync(RoomBookingInfo roomBookingInfo);
        Task<bool> RemoveRoomBookingAsync(RoomBookingInfo roomBookingInfo);
        Task<ICollection<RoomInfo>> GetAvailableRoomsAsync(RoomBookingPeriod roomBookingSearch);
    }
}
