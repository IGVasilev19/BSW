using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var data = _service.GetAllEmployees();
            return View(data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _service.CreateEmployee(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }
    }
}
