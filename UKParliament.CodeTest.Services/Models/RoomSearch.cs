using System;
using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Services.Models
{
    public class RoomSearch
    {
        [Required]
        public string Name { get; set; }
    }
}
