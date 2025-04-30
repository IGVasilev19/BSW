using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseManagementSystem.Controllers
{
    public class SystemController : Controller
    {
        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        public IActionResult Orders()
        {
            return View();
        }

        [Authorize]
        public IActionResult Employees()
        {
            return View();
        }

        [Authorize]
        public IActionResult Inventory()
        {
            return View();
        }

        [Authorize]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignOutEmployee()
        {
            await HttpContext.SignOutAsync("WarehouseCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}
