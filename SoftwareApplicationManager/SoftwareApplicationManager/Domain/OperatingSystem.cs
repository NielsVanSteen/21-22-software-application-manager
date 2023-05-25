using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftwareApplicationManager.BL.Domain
{
    public class OperatingSystem
    {
        // Properties.
        [Key]
        public int? OperatingSystemId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string BannerImage { get; set; }
        public ICollection<SoftwareApplication> SoftwareApplications { get; set; }

        // Constructor.
        public OperatingSystem()
        {
            SoftwareApplications = new List<SoftwareApplication>();
        } // OperatingSystem.
        public OperatingSystem(string name, DateTime releaseDate, string description, string bannerImage) : this()
        {
            Description = description;
            Name = name;
            ReleaseDate = releaseDate;
            BannerImage = bannerImage;
        } // OperatingSystem.

        // Methods.

    }
}