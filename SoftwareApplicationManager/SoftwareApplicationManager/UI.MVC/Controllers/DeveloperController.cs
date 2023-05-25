using Microsoft.AspNetCore.Mvc;
using SoftwareApplicationManager.BL;
using SoftwareApplicationManager.BL.Domain;

namespace SoftwareApplicationManager.UI.MVC.Controllers
{
    public class DeveloperController : AbstractController
    {
        
        // Constructor.
        public DeveloperController(IManager manager) : base(manager) {}
        
        // Methods.
        public IActionResult Index()
        {
            return View();
        } // Index.
        
        public IActionResult Detail(int id)
        {
            //Developer developer = base.Manager.GetDeveloper(id, true, true);
            //return View(developer);
            return View();
        } // Detail.
        
    }
}