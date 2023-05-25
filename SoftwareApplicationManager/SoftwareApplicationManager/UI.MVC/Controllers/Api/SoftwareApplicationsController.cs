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
    public class SoftwareApplicationsController : ControllerBase
    {
        // Fields
        private readonly IManager _manager;
        
        // Properties.
        public IManager Manager
        {
            get { return _manager;  }
        }
        
        // Constructor.
        public SoftwareApplicationsController(IManager manager)
        {
            _manager = manager;
        } // DevelopersController.
        
        // Methods
        // Return all Software Applications.
        [HttpGet]
        public IActionResult Get()
        {
            // Get all developers, and check if result yielded any developers.
            var applications = _manager.GetAllSoftwareApplications();
            
            if (applications == null || !applications.Any())
                return NoContent();

            // Convert result to a serializable DTO (Data Transfer Object).
            ICollection<SoftwareApplicationDto> softwareApplicatonDtos = new List<SoftwareApplicationDto>();
            foreach (var application in applications)
            {
                softwareApplicatonDtos.Add(new SoftwareApplicationDto(application));
            } // Foreach.

            return Ok(softwareApplicatonDtos);
        } // Get.
        
        // Return a specific application.
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            SoftwareApplication softwareApplication = _manager.GetSoftwareApplication(id);
            
            if (softwareApplication == null)
                return NoContent();
            
            return Ok(new SoftwareApplicationDto(softwareApplication));
        } // Get.

        
        
    }
}