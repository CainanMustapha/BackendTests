using System;
using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Services.Models
{
    public class RoomBookingPeriod
    {
        [Required]
        public DateTime AvailableFrom { get; set; }
        [Required]
        public DateTime AvailableTo { get; set; }
    }
}
