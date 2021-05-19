using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IUpdatePerson
    {
        Task<PersonInfo> UpdatePersonAsync(PersonInfo personInfo);
    }
}
