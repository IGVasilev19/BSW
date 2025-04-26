using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WarehouseManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
