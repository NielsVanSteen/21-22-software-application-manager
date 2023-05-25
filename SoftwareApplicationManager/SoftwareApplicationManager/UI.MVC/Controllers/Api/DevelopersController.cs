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
    public class DevelopersController : ControllerBase
    {
        // Fields
        private readonly IManager _manager;
        
        // Properties.
        public IManager Manager
        {
            get { return _manager;  }
        }
        
        // Constructor.
        public DevelopersController(IManager manager)
        {
            _manager = manager;
        } // DevelopersController.
        
        // Methods.
        
        // Return all Developers.
        [HttpGet]
        public IActionResult Get()
        {
            // Get all developers, and check if result yielded any developers.
            var developers = _manager.GetAllDevelopers();
            
            if (developers == null || !developers.Any())
                return NoContent();

            // Convert result to a serializable DTO (Data Transfer Object).
            ICollection<DeveloperDto> developerDtos = new List<DeveloperDto>();
            foreach (var developer in developers)
            {
                developerDtos.Add(new DeveloperDto(developer));
            } // Foreach.

            return Ok(developerDtos);
        } // Get.
        
        // Return a specific Developer.
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Developer developer = _manager.GetDeveloper(id);
            
            if (developer == null)
                return NoContent();
            
            return Ok(new DeveloperDto(developer));
        } // Get.
        
        // Create a Developer.
        [HttpPost]
        public IActionResult Post([FromBody] Developer developer)
        {
            // Modelstate.Valid() happens automatically.
            
            if (_manager.DoesDeveloperExist(developer.DeveloperId))
                return Conflict(developer);

            _manager.AddDeveloper(developer);
            
            return CreatedAtAction("Get", new {controller = "Developers", id = developer.DeveloperId}, developer);
        } // Post.
        
        // Update a Developer.
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Developer developer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Values not right");
            
            if (id != developer.DeveloperId)
                return BadRequest("Id doesn't match");
            
            if (!_manager.DoesDeveloperExist(developer.DeveloperId))
                return NotFound();

            _manager.ChangeDeveloper(developer);
            return NoContent();
        } // Put.
        
    }
}