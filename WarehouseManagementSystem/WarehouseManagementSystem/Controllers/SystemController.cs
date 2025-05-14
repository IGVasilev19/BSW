using Domain;
using Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using System.Reflection;
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
            var loggedEmployeeEmail = @User.FindFirst(ClaimTypes.Email)?.Value;

            var loggedEmployee = await _employeeService.GetByEmailAsync(loggedEmployeeEmail);

            var employees = await _employeeService.GetAllAsync(loggedEmployee.EmployeeId, loggedEmployee.WarehouseId);

            var vm = new EmployeesPageViewModel
            {
                Employees = employees.Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Role = e.Role,
                    IsActive = e.IsActive
                }).ToList()
            };

            return View(vm);
        }

        [Authorize]
        public IActionResult Settings()
        {
            return View();
        }

        [Authorize]
        public IActionResult CreateEmployeeView()
        {
            var vm = new CreateEmployeeViewModel
            {
                Roles = new SelectList(
                    Enum.GetValues(typeof(Role))
                        .Cast<Role>()
                        .Where(r => r != Role.Admin)
                        .Select(r => new { Value = (int)r, Text = r.ToString() }),
                    "Value", "Text"
                )
            };

            return View("CreateEmployee",vm);
        }

        [HttpPost]
        public async Task<IActionResult> SignOutEmployee()
        {
            _employeeService.UpdateActivityAsync(@User.FindFirst(ClaimTypes.Email)?.Value, false);
            await HttpContext.SignOutAsync("WarehouseCookie");

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> EditEmployeeView(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            var vmEmployee = new EditEmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
                SelectedRole = (int)employee.Role,
                Roles = new SelectList(
                    Enum.GetValues(typeof(Role))
                        .Cast<Role>()
                        .Where(r => r != Role.Admin)
                        .Select(r => new { Value = (int)r, Text = r.ToString() }),
                    "Value", "Text"
                )
            };

            return View("EditEmployee",vmEmployee);
        }
    }
}
