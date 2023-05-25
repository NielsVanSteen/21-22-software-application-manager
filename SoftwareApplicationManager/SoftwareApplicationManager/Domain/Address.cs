using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareApplicationManager.BL.Domain
{
    public class Address
    {
        // Properties.
        //public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        public Developer Addressee { get; set; }

        // Constructor.
        public Address(string street, int number, string postalCode, string city, string country)
        {
            Street = street;
            Number = number;
            PostalCode = postalCode;
            City = city;
            Country = country;
        } // Address.

        public Address(string street, int number, string postalCode, string city, string country, Developer addressee) : this(street, number, postalCode, city, country)
        {
            Addressee = addressee;
        } // Address.

        // Methods.
    }
}