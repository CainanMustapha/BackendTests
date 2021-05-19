using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IRoomDataStoreRepo
    {
        Task<RoomRepoActionResult> CreateRoomAsync(Room room);

        Task<RoomRepoActionResult> DeleteRoomAsync(Room room);

        Task<RoomRepoActionResult> FindRoomAsync(RoomSearch roomSearch);

        Task<RoomRepoActionResult> GetRoomByIdAsync(int id);

        Task<RoomRepoActionResult> UpdateRoomAsync(Room room);
    }
}
