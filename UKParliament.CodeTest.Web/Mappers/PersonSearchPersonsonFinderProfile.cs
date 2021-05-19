using AutoMapper;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Mappers
{
    public class PersonSearchPersonsonFinderProfile : Profile
    {
        public PersonSearchPersonsonFinderProfile()
        {
            CreateMap<PersonSearch, PersonFinder>();
        }
    }
}
