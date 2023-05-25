using System;
using System.ComponentModel.DataAnnotations;

namespace SoftwareApplicationManager.BL.Domain
{
    public class Rating
    {
        // Properties.
        //[Key]
        //public int RatingId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(0, 10)]
        public double Score { get; set; }
        public DateTime PostedOn { get; set; }
        
        [Required]
        public Developer Developer { get; set; }
        [Required]
        public SoftwareApplication SoftwareApplication { get; set; }

        // Constructor.
        public Rating() {}
        public Rating(string title, double score)
        {
            Title = title;
            Score = score;
        } // Rating.

        public Rating(string title, double score, DateTime postedOn, string description = null)
        {
            Title = title;
            Description = description;
            Score = score;
            PostedOn = postedOn;
        } // Rating.
        
        public Rating(string title, double score, DateTime postedOn, Developer developer, SoftwareApplication softwareApplication, string description = null) : this(title, score, postedOn, description)
        {
            Developer = developer;
            SoftwareApplication = softwareApplication;
        } // Rating.
        
        // Methods.
        
        
    }
}