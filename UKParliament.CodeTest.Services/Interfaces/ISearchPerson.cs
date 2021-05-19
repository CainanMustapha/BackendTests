using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface ISearchPerson
    {
        Task<PersonInfo> SearchPersonAsync(PersonSearch personSearch);
    }
}
