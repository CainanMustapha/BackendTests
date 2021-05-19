using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IPersonService: IAddPerson, IUpdatePerson, ISearchPerson, IDeletePerson
    {
        Task<PersonInfo> GetAsync(int personId);
    }
}