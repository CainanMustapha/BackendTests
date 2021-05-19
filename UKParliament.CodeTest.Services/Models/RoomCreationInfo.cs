using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Services.Models
{
    public class RoomCreationInfo
    {
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
