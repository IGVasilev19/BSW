using System.Diagnostics;
using Entities;
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
            return View(model);

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

    [Route("access/sign-up")]
    public IActionResult SignUp()
    {
        return View();
    }

    [Route("access/sign-in")]
    public IActionResult SignIn()
    {
        return View();
    }
}
