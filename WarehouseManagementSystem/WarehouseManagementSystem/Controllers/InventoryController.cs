using System.Diagnostics;
using System.Security.Claims;
using Domain;
using Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IZoneService _zoneService;
        private readonly IEmployeeService _employeeService;

        public InventoryController (IProductService productService, ICategoryService categoryService, IZoneService zoneService, IEmployeeService employeeService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _zoneService = zoneService;
            _employeeService = employeeService;
        }

        [Authorize]
        public IActionResult Inventory()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CreateProductView()
        {
            List<Category> categories = (List<Category>)await _categoryService.GetAllAsync();
            var availableCategories = new List<CreateCategoryViewModel>();

            foreach (var category in categories) 
            {
                var availableCategory = new CreateCategoryViewModel
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name
                };

                availableCategories.Add(availableCategory);
            }

            var vm = new CreateProductViewModel
            {
                Categories = availableCategories,
            };

            return View("CreateProduct", vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct (CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                 return View("CreateProduct", model);
            }

            var newProduct = new Product( 
                model.Name, 
                model.Price, 
                model.SelectedCategory.CategoryId 
            );

            try
            {
                _productService.CreateAsync(newProduct);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", "This product already exists");

                return View("CreateProduct", model);
            }

            return View("Inventory");
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult CreateCategoryView()
        {
            return View("CreateCategory");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateCategory", model);
            }

            var newCategory = new Category(model.Name);

            try
            {
                await _categoryService.CreateAsync(newCategory);
            }
            catch(QueryFailedException ex)
            {
                ModelState.AddModelError("Name", "This category already exists");

                return View("CreateCategory", model);
            }

            return View("Inventory");
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult CreateZoneView()
        {
            return View("CreateZone");
        }

        [HttpPost]
        public async Task<IActionResult> CreateZone(CreateZoneViewModel model)
        {
            if (!ModelState.IsValid)
            {
                 return View("CreateZone", model);
            }

            var loggedEmployeeEmail = @User.FindFirst(ClaimTypes.Email)?.Value;

            var loggedEmployee = await _employeeService.GetByEmailAsync(loggedEmployeeEmail);

            var newZone = new Zone(
                model.Name,
                model.Capacity,
                loggedEmployee.WarehouseId
            );

            try
            {
                _zoneService.CreateAsync(newZone);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Name", "This zone already exists");

                return View("CreateZone", model);
            }
            
            return View("Inventory");
        }
    }
}
