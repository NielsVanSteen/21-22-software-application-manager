using System;
using System.ComponentModel.DataAnnotations;
using SoftwareApplicationManager.BL.Domain;

namespace SoftwareApplicationManager.UI.MVC.Models.Dto
{
    public class RatingDto
    {
        // Properties.
        public string Title { get; set; }
        public string Description { get; set; }
        public double Score { get; set; }
        public DateTime PostedOn { get; set; }

        public int DeveloperId { get; set; }
        public int SoftwareApplicationId { get; set; }

        // Constructor.
        public RatingDto() {}
        public RatingDto(string title, string description, double score, DateTime postedOn, int developerId, int softwareApplicationId)
        {
            Title = title;
            Description = description;
            Score = score;
            PostedOn = postedOn;
            DeveloperId = developerId;
            SoftwareApplicationId = softwareApplicationId;
        } // RatingDto.

        public RatingDto(Rating rating)
        {
            Title = rating.Title;
            Description = rating.Description;
            Score = rating.Score;
            PostedOn = rating.PostedOn;
        } // RatingDto.

        public Rating ForgeRating(SoftwareApplication softwareApplication, Developer developer)
        {
            return new Rating(Title, Score, PostedOn, developer, softwareApplication, Description);
        } // ForgeRating.
    }
}