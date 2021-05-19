using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Services
{
    public class BookingsService : IBookingsService
    {
        private readonly IBookingDataStoreRepo _bookingDataStoreRepo;
        private readonly IMapper _mapper;

        public BookingsService(IBookingDataStoreRepo bookingDataStoreRepo, IMapper mapper)
        {
            _bookingDataStoreRepo = bookingDataStoreRepo;
            _mapper = mapper;
        }

        public async Task<RoomBooking> AddRoomBookingAsync(RoomBookingInfo roomBookingInfo)
        {
            var roomBooking = _mapper.Map<RoomBooking>(roomBookingInfo);
            var result = await _bookingDataStoreRepo.AddBookingAsync(roomBooking);

            if (result.Success && result?.RoomBooking != null)
            {
                return result.RoomBooking;
            }

            return null;
        }

        public async Task<ICollection<RoomInfo>> GetAvailableRoomsAsync(RoomBookingPeriod roomBookingSearch)
        {
            var result = await _bookingDataStoreRepo.FindAvailableRoomsAsync(roomBookingSearch);

            if (result.Success && result.AvailableRooms != null)
            {
                var availableRooms = _mapper.Map<List<RoomInfo>>(result.AvailableRooms);
                return availableRooms;
            }

            return null;
        }

        public async Task<bool> RemoveRoomBookingAsync(RoomBookingInfo roomBookingInfo)
        {
            var roomBooking = _mapper.Map<RoomBooking>(roomBookingInfo);
            var result = await _bookingDataStoreRepo.DeleteBookingAsync(roomBooking);

            return result.Success;
        }
    }
}
