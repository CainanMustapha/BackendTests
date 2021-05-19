using System.ComponentModel.DataAnnotations;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Services.Models
{
    public class RoomBookingInfo
    {
        [Required]
        public int PersonId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 5)]
        public string PostCode { get; set; }

        [Required]
        public RoomBookingPeriod Period { get; set; }

        [Required]
        public string RoomName { get; set; }
    }
}
