using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Services.Models
{
    public class RoomRepoActionResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public Room Room { get; set; } = null;
    }
}
