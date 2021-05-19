using AutoMapper;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Mappers
{
    public class RoomCreationInfoRoomProfile : Profile
    {
        public RoomCreationInfoRoomProfile()
        {
            CreateMap<RoomCreationInfo, Room>();
        }
    }
}
