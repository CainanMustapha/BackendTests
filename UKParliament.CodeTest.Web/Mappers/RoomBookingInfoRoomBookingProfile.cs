using AutoMapper;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Mappers
{
    public class RoomBookingInfoRoomBookingProfile : Profile
    {
        public RoomBookingInfoRoomBookingProfile()
        {
            CreateMap<RoomBookingInfo, RoomBooking>()
                .ForPath(rb => rb.Person.LastName,
                opt => opt.MapFrom(src => src.LastName))
                .ForPath(rb => rb.Person.Name,
                opt => opt.MapFrom(src => src.Name))
                .ForPath(rb => rb.Person.Id,
                opt => opt.MapFrom(src => src.Period))
                .ForPath(rb => rb.Room.Name,
                opt => opt.MapFrom(src => src.RoomName));
        }
    }
}
