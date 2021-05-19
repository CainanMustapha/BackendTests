﻿using AutoMapper;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Mappers
{
    public class RoomSearchRoomProfile : Profile
    {
        public RoomSearchRoomProfile()
        {
            CreateMap<RoomSearch, Room>();
        }
    }
}
