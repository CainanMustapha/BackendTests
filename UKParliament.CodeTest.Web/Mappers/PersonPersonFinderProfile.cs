using AutoMapper;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Mappers
{
    public class PersonPersonFinderProfile : Profile
    {
        public PersonPersonFinderProfile()
        {
            CreateMap<Person, PersonFinder>().ReverseMap();
        }
    }
}
