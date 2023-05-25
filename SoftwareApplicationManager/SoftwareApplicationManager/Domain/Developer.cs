using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftwareApplicationManager.BL.Domain
{
    public class Developer : IValidatableObject
    {
        // Properties.
        [Key]
        public int DeveloperId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<SoftwareApplication> DevelopedApplications { get; set; }
        public ICollection<Rating> RatedApplications { get; set; }
        public Address Address { get; set; }
        
        // Constructors.
        public Developer()
        {
            DevelopedApplications = new List<SoftwareApplication>();
            RatedApplications = new List<Rating>();
        } // Developer.
        public Developer(string name, string description, string profilePicture, string phoneNumber, string email, DateTime birthDate, Address address) : this(name, description, profilePicture, phoneNumber, email, birthDate)
        {
            Address = address;
        } // Developer.

        public Developer(int developerId, string name, string description, DateTime birthDate, string profilePicture, string phoneNumber, string email)
        :this(name, description ,profilePicture, phoneNumber, email, birthDate)
        {
            this.DeveloperId = developerId;
        }

        public Developer(string name, string description, string profilePicture, string phoneNumber, string email, DateTime birthDate) : this()
        {
            Name = name;
            Description = description;
            ProfilePicture = profilePicture;
            PhoneNumber = phoneNumber;
            Email = email;
            BirthDate = birthDate;
        } // Developer.
        
        // Methods.
        
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            
            if (!Email.Contains("@") || !Email.Contains("."))
                result.Add(new ValidationResult("Email must be valid! Use at least 1 '@' and 1 '.'.",
                    new string[] {"Email"}));
            
            return result;
        } // IValidatableObject.Validate.
        
    }
}