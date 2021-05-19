using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Repo
{
    public class BookingsDataStoreRepo: IBookingDataStoreRepo
    {
        public DbSet<Room> Rooms { get; private set; }

        private readonly RoomBookingRepoActionResult _roomBookingRepoActionResult;
        private readonly RoomBookingsContext _roomBookingsContext;

        public BookingsDataStoreRepo(
            RoomBookingsContext roomBookingsContext,
            RoomBookingRepoActionResult roomBookingRepoActionResult)
        {
            _roomBookingsContext = roomBookingsContext;
            Rooms = _roomBookingsContext.Rooms;
            _roomBookingRepoActionResult = roomBookingRepoActionResult;
        }

        public async Task<RoomBookingRepoActionResult> AddBookingAsync(RoomBooking roomBooking)
        {
            if (!await IsRoomBookedAsync(roomBooking.Room))
            {
                var entity = await Rooms.FindAsync(roomBooking.Room);
                entity.Bookings.Add(roomBooking);
                var updated = Rooms.Update(entity);

                if (await updated.Context.SaveChangesAsync() == 1)
                {
                    _roomBookingRepoActionResult.RoomBooking = roomBooking;
                    _roomBookingRepoActionResult.Success = true;
                }
                else
                {
                    _roomBookingRepoActionResult.Message = Constants.ErrorMessages.DataRepo.ErrorAddingEntity;
                }
            }
            else
            {
                _roomBookingRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityExist;
            }

            return _roomBookingRepoActionResult;
        }

        public async Task<RoomBookingRepoActionResult> FindAvailableRoomsAsync(RoomBookingPeriod roomBookingSearch)
        {
            var entity = await Task.Run(() => Rooms.Where(
                r => r.Bookings.Count == 0 | r.Bookings.Any(r => r.AvailableFrom.Subtract(DateTime.UtcNow).TotalMinutes > 60)).ToList());

            if (entity != null)
            {
                _roomBookingRepoActionResult.AvailableRooms = entity;
                _roomBookingRepoActionResult.Success = true;
            }
            else
            {
                _roomBookingRepoActionResult.Message = Constants.ErrorMessages.DataRepo.NoRoomAvailable;
            }

            return _roomBookingRepoActionResult;
        }

        public async Task<RoomBookingRepoActionResult> DeleteBookingAsync(RoomBooking roomBooking)
        {
            var entity = await Rooms.FindAsync(roomBooking.Room.Name);

            if (entity != null)
            {
                var entry = _roomBookingsContext.Entry(entity);

                if(entry.State != EntityState.Modified)
                {
                    entity.Bookings.Remove(roomBooking);
                    entry.State = EntityState.Modified;

                    if (await _roomBookingsContext.SaveChangesAsync() == 1)
                    {
                        _roomBookingRepoActionResult.Success = true;
                    }
                    else
                    {
                        _roomBookingRepoActionResult.Message = Constants.ErrorMessages.DataRepo.BookingRemovalFailed;
                    }
                }
                else
                {
                    _roomBookingRepoActionResult.Message = Constants.ErrorMessages.DataRepo.BookingRemovalFailed;
                }
            }

            return _roomBookingRepoActionResult;
        }

        private async Task<bool> IsRoomBookedAsync(Room room)
        {
            var entity = await Rooms.FindAsync(room.Name);

            if (entity != null)
            {
                var bookings = entity.Bookings;

                if(bookings.Any(r => r.AvailableFrom.Subtract(DateTime.UtcNow).TotalMinutes > 60))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
