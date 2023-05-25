using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq;

namespace SoftwareApplicationManager.BL.Domain
{
    public class SoftwareApplication : IValidatableObject
    {
        // Properties.
        [Key]
        public int? SoftwareApplicationId { get; set; }
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
        [Range(0, 10)]
        public double? Score { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Required]
        public String Description { get; set; }
        public String  ImagePath { get; set; }
        [Required]
        public AppType.Type ApplicationType { get; set; }
        [Range(0, Int32.MaxValue)]
        public int Downloads { get; set; }
        public Developer Developer { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<OperatingSystem> AvailableOnOperationSystems { get; set; }

        // Constructors.
        public SoftwareApplication()
        {
            AvailableOnOperationSystems = new List<OperatingSystem>();
            Ratings = new List<Rating>();
        } // SoftwareApplication.
        public SoftwareApplication(string name, double? score, String description, String imagePath, DateTime releaseDate, AppType.Type applicationType, int downloads, Developer developer) 
            : this(name, score, description, imagePath, releaseDate, applicationType, downloads)
        {
            Developer = developer;
        } // SoftwareApplication.
        public SoftwareApplication(string name, double? score, String description, String imagePath, DateTime releaseDate, AppType.Type applicationType, int downloads) : this()
        {
            Name = name;
            Score = score;
            Description = description;
            ImagePath = imagePath;
            ReleaseDate = releaseDate;
            ApplicationType = applicationType;
            Downloads = downloads;
        } // SoftwareApplication.
        
       

        // Methods.

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            
            var values = Enum.GetValues(typeof(AppType.Type)).Cast<AppType.Type>().ToList();

            var test = ApplicationType;
            
            if (!values.Contains(ApplicationType))
                result.Add(new ValidationResult("Enum type must be valid!"));
            
            return result;
        } // IValidatableObject.Validate.
    }
}