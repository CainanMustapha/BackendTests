using AutoMapper;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Mappers
{
    public class RoomRoomInfoProfile : Profile
    {
        public RoomRoomInfoProfile()
        {
            CreateMap<Room, RoomInfo>().ReverseMap();
        }
    }
}
