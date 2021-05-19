using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IRoomService
    {
        Task<RoomInfo> AddRoomAsync(RoomCreationInfo roomCreationInfo);
        Task<bool> DeleteRoomAsync(RoomSearch roomSearch);
        Task<RoomInfo> GetAsync(int roomId);
        Task<RoomInfo> SearchRoomAsync(RoomSearch roomSearch);
        Task<RoomInfo> UpdateRoomAsync(RoomInfo roomInfo);
    }
}
