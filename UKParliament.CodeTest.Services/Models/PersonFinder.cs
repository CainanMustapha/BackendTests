using System;

namespace UKParliament.CodeTest.Services.Models
{
    public class PersonFinder
    {
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public int? Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }
    }
}
