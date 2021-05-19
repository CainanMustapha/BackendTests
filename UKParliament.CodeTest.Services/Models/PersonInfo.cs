using System;
using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Services.Models
{
    public class PersonInfo
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 5)]
        public string PostCode { get; set; }
    }
}