using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WarehouseManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "System");
            }
            
            return View();
        }
    }
}
