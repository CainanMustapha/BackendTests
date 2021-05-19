using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IAddPerson
    {
        Task<PersonInfo> AddPersonAsync(PersonCreationInfo personInfo);
    }
}
