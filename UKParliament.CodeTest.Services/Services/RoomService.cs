using AutoMapper;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomDataStoreRepo _roomDataStoreRepo;
        private readonly IMapper _mapper;

        public RoomService(IRoomDataStoreRepo roomDataStoreRepo, IMapper mapper)
        {
            _roomDataStoreRepo = roomDataStoreRepo;
            _mapper = mapper;
        }

        public async Task<RoomInfo> GetAsync(int personId)
        {
            var result = await _roomDataStoreRepo.GetRoomByIdAsync(personId);

            if(result.Success && result.Room != null)
            {
                var personInfo = _mapper.Map<RoomInfo>(result.Room);

                return personInfo;
            }

            return null;           
        }

        public async Task<RoomInfo> AddRoomAsync(RoomCreationInfo roomCreationInfo)
        {
            var newRoom = _mapper.Map<Room>(roomCreationInfo);
            var result = await _roomDataStoreRepo.CreateRoomAsync(newRoom);

            if (result.Success && result.Room != null)
            {
                var createdRoomInfo = _mapper.Map<RoomInfo>(result.Room);

                return createdRoomInfo;
            }

            return null;
        }

        public async Task<RoomInfo> UpdateRoomAsync(RoomInfo roomInfo)
        {
            var room = _mapper.Map<Room>(roomInfo);
            var result = await _roomDataStoreRepo.UpdateRoomAsync(room);

            if (result.Success && result.Room != null)
            {
                var updatePersonInfo = _mapper.Map<RoomInfo>(result.Room);

                return updatePersonInfo;
            }

            return null;
        }

        public async Task<RoomInfo> SearchRoomAsync(RoomSearch roomSearch)
        {
            var result = await _roomDataStoreRepo.FindRoomAsync(roomSearch);

            if (result.Success && result.Room != null)
            {
                var person = _mapper.Map<RoomInfo>(result.Room);

                return person;
            }

            return null;
        }

        public async Task<bool> DeleteRoomAsync(RoomSearch roomSearch)
        {
            var room = _mapper.Map<Room>(roomSearch);
            var result = await _roomDataStoreRepo.DeleteRoomAsync(room);

            return result.Success;
        }
    }
}
