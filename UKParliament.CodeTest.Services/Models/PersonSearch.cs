using System;
using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Services.Models
{
    public class PersonSearch
    {
        [Required]
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PostCode { get; set; }
    }
}
