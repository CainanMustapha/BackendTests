using AutoMapper;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Mappers
{
    public class PersonInfoPersonProfile : Profile
    {
        public PersonInfoPersonProfile()
        {
            CreateMap<PersonInfo, Person>().ReverseMap();
        }
    }
}
