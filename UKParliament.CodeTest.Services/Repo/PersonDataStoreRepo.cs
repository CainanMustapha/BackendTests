using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Repo
{
    public class PersonDataStoreRepo: IPersonDataStoreRepo
    {
        public DbSet<Person> People { get; private set; }

        private readonly PersonRepoActionResult _personRepoActionResult;
        private readonly IMapper _mapper;
        private readonly RoomBookingsContext _roomBookingsContext;

        public PersonDataStoreRepo(
            RoomBookingsContext roomBookingsContext,
            PersonRepoActionResult personRepoActionResult,
            IMapper mapper)
        {
            _roomBookingsContext = roomBookingsContext;
            People = _roomBookingsContext.People;
            _personRepoActionResult = personRepoActionResult;
            _mapper = mapper;
        }

        public async Task<PersonRepoActionResult> CreatePersonAsync(Person person)
        {
            var personFinder = _mapper.Map<PersonFinder>(person);

            if (!await PersonExistAsync(personFinder))
            {
                var entity = await People.AddAsync(person);

                if (await entity.Context.SaveChangesAsync() == 1)
                {
                    _personRepoActionResult.Person = entity.Entity;
                    _personRepoActionResult.Success = true;
                }
                else
                {
                    _personRepoActionResult.Message = Constants.ErrorMessages.DataRepo.ErrorAddingEntity;
                }
            }
            else
            {
                _personRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityExist;
            }

            return _personRepoActionResult;
        }

        public async Task<PersonRepoActionResult> FindPersonAsync(PersonFinder personFinder)
        {
            var entity = People.Where(p => 
            p.DateOfBirth == personFinder.DateOfBirth &&
            p.LastName == personFinder.LastName &&
            p.Name == personFinder.Name &&
            p.PostCode == personFinder.PostCode).FirstOrDefault();

            if (entity != null)
            {
                _personRepoActionResult.Person = entity;
                _personRepoActionResult.Success = true;
            }
            else
            {
                _personRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityDoesNotExist;
            }

            return _personRepoActionResult;
        }

        public async  Task<PersonRepoActionResult> GetPersonByIdAsync(int id)
        {
            var entity = await People.FirstOrDefaultAsync(p => p.Id == id);

            if(entity != null)
            {
                _personRepoActionResult.Person = entity;
                _personRepoActionResult.Success = true;
            }
            else
            {
                _personRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityDoesNotExist;
            }

            return _personRepoActionResult;
        }

        public async Task<PersonRepoActionResult> UpdatePersonAsync(Person person)
        {
            var personFinder = _mapper.Map<PersonFinder>(person);

            if (await PersonExistAsync(personFinder))
            {
                var entity = await People.FirstAsync(p => p.Id == person.Id);
                /// Only Phone allowed
                entity.Phone = person.Phone;
                var updatedEntity = _roomBookingsContext.Update(entity);

                if (await _roomBookingsContext.SaveChangesAsync() == 1)
                {
                    _personRepoActionResult.Person = updatedEntity.Entity;
                    _personRepoActionResult.Success = true;
                }
                else
                {
                    _personRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityUpdateFailed;
                }
            }

            return _personRepoActionResult;
        }

        public async Task<PersonRepoActionResult> DeletePersonAsync(Person person)
        {
            var entity = await People.FindAsync(person.Id, person.DateOfBirth, person.Name, person.LastName, person.PostCode);

            if (entity != null)
            {
                var entry = _roomBookingsContext.Entry(entity);

                if(entry.State != EntityState.Modified)
                {
                    var entityEntry = People.Remove(entity);
                    /// Double take
                    entityEntry.State = EntityState.Deleted;

                    if (await _roomBookingsContext.SaveChangesAsync() == 1)
                    {
                        _personRepoActionResult.Success = true;
                    }
                    else
                    {
                        _personRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityDeleteFailed;
                    }
                }
                else
                {
                    _personRepoActionResult.Message = Constants.ErrorMessages.DataRepo.EntityDeletionNotPermitted;
                }
            }

            return _personRepoActionResult;
        }

        private async Task<bool> PersonExistAsync(PersonFinder personFinder)
        {
            var entity = await People.FindAsync(personFinder.Id, personFinder.DateOfBirth, personFinder.Name, personFinder.LastName, personFinder.PostCode);

            if (entity != null)
            {
                return true;
            }

            return false;
        }
    }
}
