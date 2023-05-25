using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SoftwareApplicationManager.BL;
using SoftwareApplicationManager.BL.Domain;
using SoftwareApplicationManager.UI.MVC.Models.Dto;

namespace SoftwareApplicationManager.UI.MVC.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        // Fields
        private readonly IManager _manager;
        
        // Properties.
        public IManager Manager
        {
            get { return _manager;  }
        }
        
        // Constructor.
        public RatingsController(IManager manager)
        {
            _manager = manager;
        } // RatingsController.
        
        // Methods
        
        // Get All Ratings of a developer.
        [HttpGet("{developerId}")]
        public IActionResult Get(int developerId)
        {
            var developer = _manager.GetDeveloper(developerId, false, true);
            var ratings = developer.RatedApplications;

            if (ratings == null || !ratings.Any())
                return NoContent();

            // Convert result to a serializable DTO (Data Transfer Object).
            ICollection<RatingDto> ratingDtos = new List<RatingDto>();
            foreach (var rating in ratings)
            {
                ratingDtos.Add(new RatingDto(rating));
            } // Foreach.

            return Ok(ratingDtos);
        } // Get.
        
        // Create a Rating.
        [HttpPost]
        public IActionResult Post([FromBody] RatingDto rating)
        {
            Developer developer = _manager.GetDeveloper(rating.DeveloperId);
            SoftwareApplication softwareApplication = _manager.GetSoftwareApplication(rating.SoftwareApplicationId);
            Rating r = rating.ForgeRating(softwareApplication, developer);
            
            _manager.AddRating(r);

            return Created(rating.Title, rating);
        } // Post.
    }
}