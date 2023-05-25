using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SoftwareApplicationManager.BL.Domain;

namespace SoftwareApplicationManager.UI.MVC.Models
{
    public class AddSoftwareApplicationModel
    {
        // Properties.
        public SoftwareApplication SoftwareApplication { get; set; }
        public long? DeveloperId { get; set; }
        [Required]  
        [Display(Name = "Profile Picture")]  
        public IFormFile Image { get; set; }  

        // Constructor.
        public AddSoftwareApplicationModel() {}
        public AddSoftwareApplicationModel(SoftwareApplication softwareApplication)
        {
            SoftwareApplication = softwareApplication;
        } // AddSoftwareApplicationModel.
    }
}