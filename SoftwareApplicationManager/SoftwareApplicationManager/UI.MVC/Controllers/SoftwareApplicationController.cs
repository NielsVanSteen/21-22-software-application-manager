using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Transactions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Hosting.Internal;
using SoftwareApplicationManager.BL;
using SoftwareApplicationManager.BL.Domain;
using SoftwareApplicationManager.UI.MVC.Models;

namespace SoftwareApplicationManager.UI.MVC.Controllers
{
    public class SoftwareApplicationController : AbstractController
    {
        // Properties.
        private readonly IWebHostEnvironment _webHostEnvironment;  
        
        // Constructor.
        public SoftwareApplicationController(IManager manager, IWebHostEnvironment webHostEnvironment) : base(manager)
        {
            _webHostEnvironment = webHostEnvironment;
        } // SoftwareApplicationController.

        // Methods.
        
        // GET
        public IActionResult Index()
        {
            IEnumerable<SoftwareApplication> softwareApplications = base.Manager.GetAllSoftwareApplications(false, true, false);
            return View(softwareApplications);
        } // Index.
        
        public IActionResult Detail(int id)
        {
            SoftwareApplication softwareApplication = base.Manager.GetSoftwareApplication(id, true, true, true, true);
            return View(softwareApplication);
        } // Detail.
        
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Developers = Manager.GetAllDevelopers();
            return View(new AddSoftwareApplicationModel(null));
        } // Add.
        
        [HttpPost]
        public IActionResult Add(AddSoftwareApplicationModel softwareApplicationModel)
        {
            // The Select list in the view only stores the 'DeveloperId' as the value in each option ->
            // So we store the selected DeveloperId in the ViewModel
            // Then when the used submits the form we check if the Id is set, if so we retrieve the developer it belongs to and add it to the Software Application.
            if (softwareApplicationModel.DeveloperId != null)
            {
                Developer developer = Manager.GetDeveloper(softwareApplicationModel.DeveloperId ?? 0);
                softwareApplicationModel.SoftwareApplication.Developer = developer;
            } // If.

            // Check if all the fields were filled in correctly.
            if (!ModelState.IsValid)
            {
                ViewBag.Developers = Manager.GetAllDevelopers();
                return View(softwareApplicationModel);
            } // If.

            // Create unique filename for image and move to correct directory.
            string uniqueFileName = UploadedFile(softwareApplicationModel);
            softwareApplicationModel.SoftwareApplication.ImagePath = uniqueFileName;

            // Show the detail page.
            Manager.AddSoftwareApplication(softwareApplicationModel.SoftwareApplication);
            return RedirectToAction("Detail", new {id = softwareApplicationModel.SoftwareApplication.SoftwareApplicationId});
        } // Create.
        
        private string UploadedFile(AddSoftwareApplicationModel model)  
        {  
            string uniqueFileName = null;  
  
            if (model.Image != null)  
            {  
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "media/images");  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    model.Image.CopyTo(fileStream);  
                }  
            }  
            return uniqueFileName;  
        } // UploadedFile.
    }
}