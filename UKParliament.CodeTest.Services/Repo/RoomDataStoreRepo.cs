using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Repo
{
    public class RoomDataStoreRepo: IRoomDataStoreRepo
    {
        public DbSet<Room> Rooms { get; private set; }

        private readonly RoomRepoActionResult _roomRepoActionResult;
        private readonly RoomBookingsContext _roomBookingsContext;

        public RoomDataStoreRepo(
            RoomBookingsContext roomBookingsContext,
            RoomRepoActionResult roomRepoActionResult)
        {
            _roomBookingsContext = roomBookingsContext;
            Rooms = _roomBookingsContext.Rooms;
            _roomRepoActionResult = roomRepoActionResult;
        }

        public async Task<RoomRepoActionResult> CreateRoomAsync(Room room)
        {
            if (!await RoomExistAsync(room))
            {
                var entity = await Rooms.AddAsync(room);

                if (await entity.Context.SaveChangesAsync() == 1)
                {
                    _roomRepoActionResult.Room = entity.Entity;
                    _roomRepoActionResult.Success = true;
                }
                else
                {
                    _roomRepoActionResult.Message = Constants.ErrorMessages.DataRepo.ErrorAddingEntity;
                }
            }
            else
            {
                _roomRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityExist;
            }

            return _roomRepoActionResult;
        }

        public async Task<RoomRepoActionResult> FindRoomAsync(RoomSearch roomSearch)
        {
            var entity = await Task.Run(() => Rooms.Where(r => r.Name == roomSearch.Name).FirstOrDefault());

            if (entity != null)
            {
                _roomRepoActionResult.Room = entity;
                _roomRepoActionResult.Success = true;
            }
            else
            {
                _roomRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityDoesNotExist;
            }

            return _roomRepoActionResult;
        }

        public async  Task<RoomRepoActionResult> GetRoomByIdAsync(int id)
        {
            var entity = await Rooms.FirstOrDefaultAsync(r => r.Id == id);

            if(entity != null)
            {
                _roomRepoActionResult.Room = entity;
                _roomRepoActionResult.Success = true;
            }
            else
            {
                _roomRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityDoesNotExist;
            }

            return _roomRepoActionResult;
        }

        public async Task<RoomRepoActionResult> UpdateRoomAsync(Room room)
        {
            if (await RoomExistAsync(room))
            {
                var entity = await Rooms.FirstAsync(r => r.Id == room.Id);
                entity.Bookings = room.Bookings;
                var updatedEntity = _roomBookingsContext.Update(entity);

                if (await _roomBookingsContext.SaveChangesAsync() == 1)
                {
                    _roomRepoActionResult.Room = updatedEntity.Entity;
                    _roomRepoActionResult.Success = true;
                }
                else
                {
                    _roomRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityUpdateFailed;
                }
            }

            return _roomRepoActionResult;
        }

        public async Task<RoomRepoActionResult> DeleteRoomAsync(Room room)
        {
            var entity = await Rooms.FindAsync(room.Name);

            if (entity != null)
            {
                var entry = _roomBookingsContext.Entry(entity);

                if(entry.State != EntityState.Modified)
                {
                    var entityEntry = Rooms.Remove(entity);
                    /// Double take
                    entityEntry.State = EntityState.Deleted;

                    if (await _roomBookingsContext.SaveChangesAsync() == 1)
                    {
                        _roomRepoActionResult.Success = true;
                    }
                    else
                    {
                        _roomRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityDeleteFailed;
                    }
                }
                else
                {
                    _roomRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityDeletionNotPermitted;
                }
            }

            return _roomRepoActionResult;
        }

        private async Task<bool> RoomExistAsync(Room room)
        {
            var entity = await Rooms.FindAsync(room.Name);

            if (entity != null)
            {
                return true;
            }

            return false;
        }
    }
}
