using Microsoft.AspNetCore.Mvc;
using SoftwareApplicationManager.BL;

namespace SoftwareApplicationManager.UI.MVC.Controllers
{
    public abstract class AbstractController : Controller
    {
        // Fields.
        private readonly IManager _manager;
        
        // Properties.
        public IManager Manager
        {
            get { return _manager;  }
        }
        
        // Constructor.
        public AbstractController(IManager manager)
        {
            _manager = manager;
        } // AbstractController.
        
        // Methods.
        
       
    }
}