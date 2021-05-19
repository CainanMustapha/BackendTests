using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Services.Models
{
    public class PersonRepoActionResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public Person Person { get; set; } = null;
    }
}
