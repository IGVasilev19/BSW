using System.Diagnostics;
using System.Security.Claims;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Service;
using WarehouseManagementSystem.Models;

public class AccessController : Controller
{
    private readonly IEmployeeService _employeeService;

    public AccessController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterOwner(OwnerRegistrationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var address = new Address(
            model.Country,
            model.City,
            model.StreetName,
            model.StreetNumber,
            model.Zip
        );

        var warehouse = new Warehouse(model.WarehouseName);

        var employee = new Employee(model.Name, model.Email, model.Password, model.PhoneNumber);

        var result = await _employeeService.RegisterOwnerWithWarehouseAsync(
            address,
            warehouse,
            employee
        );

        if (result)
            return RedirectToAction("SignIn", "Access");

        ModelState.AddModelError("", "Registration failed.");
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EmployeeSignIn(SignInViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var employee = await _employeeService.AuthenticateEmployeeAsync(model.Email, model.Password);

        if (employee == null)
        {
            ModelState.AddModelError("", "Invalid credentials.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
            new Claim(ClaimTypes.Name, employee.Name),
            new Claim(ClaimTypes.Role, employee.Role.ToString())
        };

        var identity = new ClaimsIdentity(claims, "WarehouseCookie");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("WarehouseCookie", principal);

        return RedirectToAction("Dashboard", "System");
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }
}
