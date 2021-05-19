using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Services.Models
{
    public class RoomInfo
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Name { get; set; }

        public List<RoomBooking> Bookings { get; set; }
    }
}