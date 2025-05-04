using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Employees(bool showCreateForm = false)
        {
            var loggedEmployeeEmail = @User.FindFirst(ClaimTypes.Email)?.Value;

            var loggedEmployee = await _employeeService.GetByEmailAsync(loggedEmployeeEmail);

            var employees = await _employeeService.GetAllAsync(loggedEmployee.WarehouseId);

            var vm = new EmployeesPageViewModel
            {
                Employees = employees.Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Role = e.Role,
                    IsActive = e.IsActive
                }).ToList(),
                CreateEmployee = new CreateEmployeeViewModel
                {
                    Roles = new SelectList(
                        Enum.GetValues(typeof(Role)).Cast<Role>()
                            .Where(r => r != Role.Admin)
                            .Select(r => new { Value = r, Text = r.ToString() }),
                        "Value", "Text"
                    )
                },
                ShowCreateForm = showCreateForm
            };

            return View(vm);
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

        [HttpPost]
        public async Task<IActionResult> CreateEmployee()
        {
            return RedirectToAction("Employees", "System");
        }
    }
}
