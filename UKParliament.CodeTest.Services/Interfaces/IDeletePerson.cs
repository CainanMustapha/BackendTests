using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IDeletePerson
    {
        Task<bool> DeletePersonAsync(PersonInfo personInfo);
    }
}
