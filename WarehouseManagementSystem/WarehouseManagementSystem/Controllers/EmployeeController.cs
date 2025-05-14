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
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService service)
        {
            _employeeService = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel model)
        {

            var loggedEmployeeEmail = @User.FindFirst(ClaimTypes.Email)?.Value;

            var loggedEmployee = await _employeeService.GetByEmailAsync(loggedEmployeeEmail);

            if (!ModelState.IsValid)
            {
                model.Roles = new SelectList(
                    Enum.GetValues(typeof(Role))
                        .Cast<Role>()
                        .Where(r => r != Role.Admin)
                        .Select(r => new { Value = (int)r, Text = r.ToString() }),
                    "Value", "Text"
                );

                return View("CreateEmployee", model);
            }

            var employee = new Employee(
                model.Name,
                model.Email,
                model.Password,
                model.PhoneNumber,
                (Role)model.SelectedRole,
                false,
                loggedEmployee.WarehouseId
            );

            try
            {
                await _employeeService.CreateAsync(employee);
            }
            catch (QueryFailedException ex)
            { 
                ModelState.AddModelError("Email", "Account with this email already exists");
                model.Roles = new SelectList(
                    Enum.GetValues(typeof(Role))
                        .Cast<Role>()
                        .Where(r => r != Role.Admin)
                        .Select(r => new { Value = (int)r, Text = r.ToString() }),
                    "Value", "Text"
                );

                return View("CreateEmployee", model);
            }
            
            return RedirectToAction("Employees", "System");
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = new SelectList(
                    Enum.GetValues(typeof(Role))
                        .Cast<Role>()
                        .Where(r => r != Role.Admin)
                        .Select(r => new { Value = (int)r, Text = r.ToString() }),
                    "Value", "Text"
                );

                return View("EditEmployee", model);
            }

            await _employeeService.UpdateRoleAsync(model.EmployeeId, (Role)model.SelectedRole);

            return RedirectToAction("Employees", "System");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(EditEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = new SelectList(
                    Enum.GetValues(typeof(Role))
                        .Cast<Role>()
                        .Where(r => r != Role.Admin)
                        .Select(r => new { Value = (int)r, Text = r.ToString() }),
                    "Value", "Text"
                );

                return View("EditEmployee", model);
            }

            await _employeeService.DeleteByIdAsync(model.EmployeeId);

            return RedirectToAction("Employees", "System");
        }
    }
}