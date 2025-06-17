using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Service;
using Microsoft.AspNetCore.Authorization;

namespace WarehouseManagementSystem.Controllers
{
    public class OrdersController : Controller
    {
        [Authorize]
        public IActionResult Orders()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult CreateOrder()
        {
            return View();
        }
    }
}
