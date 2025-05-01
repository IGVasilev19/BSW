using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Security.Claims;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers
{
    public class SystemController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public SystemController (IEmployeeService service)
        {
            _employeeService = service;
        }

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
        public IActionResult Inventory()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Employees()
        {
            var employees = await _employeeService.GetAllAsync();

            var viewModel = employees.Select(e => new EmployeeViewModel
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Email = e.Email,
                Role = e.Role,
                IsActive = e.IsActive
            }).ToList();

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignOutEmployee()
        {
            _employeeService.UpdateActivityAsync(@User.FindFirst(ClaimTypes.Email)?.Value, false);
            await HttpContext.SignOutAsync("WarehouseCookie");

            return RedirectToAction("Index", "Home");
        }
    }
}
