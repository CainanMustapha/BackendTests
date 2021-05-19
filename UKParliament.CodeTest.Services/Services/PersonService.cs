using AutoMapper;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonDataStoreRepo _personDataStoreRepo;
        private readonly IMapper _mapper;

        public PersonService(IPersonDataStoreRepo personDataStoreRepo, IMapper mapper)
        {
            _personDataStoreRepo = personDataStoreRepo;
            _mapper = mapper;
        }

        public async Task<PersonInfo> GetAsync(int personId)
        {
            var result = await _personDataStoreRepo.GetPersonByIdAsync(personId);

            if(result.Success && result.Person != null)
            {
                var personInfo = _mapper.Map<PersonInfo>(result.Person);

                return personInfo;
            }

            return null;           
        }

        public async Task<PersonInfo> AddPersonAsync(PersonCreationInfo personCreationInfo)
        {
            var newPersonInfo = _mapper.Map<Person>(personCreationInfo);
            var result = await _personDataStoreRepo.CreatePersonAsync(newPersonInfo);

            if (result.Success && result.Person != null)
            {
                var createdPersonInfo = _mapper.Map<PersonInfo>(result.Person);

                return createdPersonInfo;
            }

            return null;
        }

        public async Task<PersonInfo> UpdatePersonAsync(PersonInfo personInfo)
        {
            var person = _mapper.Map<Person>(personInfo);
            var result = await _personDataStoreRepo.UpdatePersonAsync(person);

            if (result.Success && result.Person != null)
            {
                var updatePersonInfo = _mapper.Map<PersonInfo>(result.Person);

                return updatePersonInfo;
            }

            return null;
        }

        public async Task<PersonInfo> SearchPersonAsync(PersonSearch personSearch)
        {
            var personFinder = _mapper.Map<PersonFinder>(personSearch);
            var result = await _personDataStoreRepo.FindPersonAsync(personFinder);

            if (result.Success && result.Person != null)
            {
                var person = _mapper.Map<PersonInfo>(result.Person);

                return person;
            }

            return null;
        }

        public async Task<bool> DeletePersonAsync(PersonInfo personInfo)
        {
            var personFinder = _mapper.Map<Person>(personInfo);
            var result = await _personDataStoreRepo.DeletePersonAsync(personFinder);

            return result.Success;
        }
    }
}
