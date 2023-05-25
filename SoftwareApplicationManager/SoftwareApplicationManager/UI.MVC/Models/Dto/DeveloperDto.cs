using System;
using SoftwareApplicationManager.BL.Domain;

namespace SoftwareApplicationManager.UI.MVC.Models.Dto
{
    public class DeveloperDto
    {
        // Properties.
        public int DeveloperId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Constructor.
        public DeveloperDto() {}
        public DeveloperDto(Developer developer)
        {
            DeveloperId = developer.DeveloperId;
            Name = developer.Name;
            Description = developer.Description;
            BirthDate = developer.BirthDate;
            ProfilePicture = developer.ProfilePicture;
            PhoneNumber = developer.PhoneNumber;
            Email = developer.Email;
        } // DeveloperDto.
        
    }
}