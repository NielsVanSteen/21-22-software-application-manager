using Microsoft.AspNetCore.Mvc;
using SoftwareApplicationManager.BL;
using SoftwareApplicationManager.BL.Domain;

namespace SoftwareApplicationManager.UI.MVC.Controllers
{
    public class OperatingSystemController : AbstractController
    {
        
        // Properties.

        // Constructor.
        public OperatingSystemController(IManager manager) : base(manager) {}
        
        // Methods.
        /*public IActionResult Index()
        {
            return View();
        } // Index.*/

        public IActionResult Detail(int id)
        {
            OperatingSystem operatingSystem = base.Manager.GetOperatingSystem(id, true, true);
            return View(operatingSystem);
        } // Detail.
    }
}