using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IPersonDataStoreRepo
    {
        Task<PersonRepoActionResult> CreatePersonAsync(Person newPerson);

        Task<PersonRepoActionResult> DeletePersonAsync(Person person);

        Task<PersonRepoActionResult> FindPersonAsync(PersonFinder personFinder);

        Task<PersonRepoActionResult> GetPersonByIdAsync(int id);

        Task<PersonRepoActionResult> UpdatePersonAsync(Person newPerson);
    }
}
