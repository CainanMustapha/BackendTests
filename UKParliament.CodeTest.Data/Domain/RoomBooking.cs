using System;
using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Data.Domain
{
    public class RoomBooking
    {
        public int Id { get; set; }

        public bool Booked { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public Person Person { get; set; }

        [Required]
        public DateTime AvailableFrom { get; set; }
        [Required]
        public DateTime AvailableTo { get; set; }

        public int RoomId { get; set; }

        [Required]
        public Room Room { get; set; }
    }
}