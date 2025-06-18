using System.Diagnostics;
using System.Security.Claims;
using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using WarehouseManagementSystem.Controllers;
using WarehouseManagementSystem.Models;

public class AccessController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<AccessController> _logger;

    public AccessController(IEmployeeService employeeService, ILogger<AccessController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterOwner(OwnerRegistrationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("SignUp",model);
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

        try
        {
            await _employeeService.RegisterOwnerWithWarehouseAsync(
            address,
            warehouse,
            employee
            );

            return RedirectToAction("SignIn", "Access");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            ViewBag.ErrorMessage = "Owner with this email already exists!";
            return View("SignUp", model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> EmployeeSignIn(SignInViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("SignIn", model);
        }

        var employee = await _employeeService.AuthenticateEmployeeAsync(model.Email, model.Password);

        if (employee == null)
        {
            ViewBag.ErrorMessage = "Invalid credentials.";
            return View("SignIn", model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
            new Claim(ClaimTypes.Role, employee.Role.ToString()),
            new Claim(ClaimTypes.Name, employee.Name),
            new Claim(ClaimTypes.Email, employee.Email)
        };

        var identity = new ClaimsIdentity(claims, "WarehouseCookie");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("WarehouseCookie", principal);

        _employeeService.UpdateActivityAsync(employee.Email, true);

        _logger.LogInformation("Employee {Email} signed in at {Time}", employee.Email, DateTime.UtcNow);

        return RedirectToAction("Dashboard", "System");
    }

    public IActionResult SignUp()
    {
        return View();
    }

    public IActionResult SignIn()
    {
        return View();
    }
}
