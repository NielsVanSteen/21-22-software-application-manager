using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SoftwareApplicationManager.BL.Domain;
using OperatingSystem = SoftwareApplicationManager.BL.Domain.OperatingSystem;

namespace SoftwareApplicationManager.UI.MVC.Models.Dto
{
    public class SoftwareApplicationDto
    {
        // Properties.
        public int? SoftwareApplicationId { get; set; }
        public string Name { get; set; }
        public double? Score { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String Description { get; set; }
        public String  ImagePath { get; set; }
        public AppType.Type ApplicationType { get; set; }
        public int Downloads { get; set; }
        
        // Constructor.
        public SoftwareApplicationDto(SoftwareApplication softwareApplication)
        {
            Name = softwareApplication.Name;
            SoftwareApplicationId = softwareApplication.SoftwareApplicationId;
            Score = softwareApplication.Score;
            ReleaseDate = softwareApplication.ReleaseDate;
            Description = softwareApplication.Description;
            ImagePath = softwareApplication.ImagePath;
            ApplicationType = softwareApplication.ApplicationType;
            Downloads = softwareApplication.Downloads;
        }
    }
}