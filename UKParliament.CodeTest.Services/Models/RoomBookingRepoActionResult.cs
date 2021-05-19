using System.Collections.Generic;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Services.Models
{
    public class RoomBookingRepoActionResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public RoomBooking RoomBooking { get; set; } = null;
        public ICollection<Room> AvailableRooms { get; set; }
    }
}
