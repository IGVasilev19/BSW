using System.Diagnostics;
using System.Security.Claims;
using Domain;
using Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Interfaces;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IZoneService _zoneService;
        private readonly IEmployeeService _employeeService;
        private readonly IInventoryService _inventoryService;

        public InventoryController (IProductService productService, ICategoryService categoryService, IZoneService zoneService, IEmployeeService employeeService, IInventoryService inventoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _zoneService = zoneService;
            _employeeService = employeeService;
            _inventoryService = inventoryService;
        }

        [Authorize]
        public async Task<IActionResult> Inventory()
        {
            var loggedUser = await _employeeService.GetByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

            List<Inventory> inventoryRecords = (List<Inventory>)await _inventoryService.GetAllAsync(loggedUser.WarehouseId);
            

            var inventories = await _inventoryService.GetAllAsync(loggedUser.WarehouseId);

            var inventoryViewModels = await Task.WhenAll(inventories.Select(async inventoryRecord =>
            {
                var product = await _productService.GetByIdAsync(inventoryRecord.ProductId);
                var zone = await _zoneService.GetByIdAsync(inventoryRecord.ZoneId);
                var category = await _categoryService.GetByIdAsync(product.CategoryId);

                return new InventoryViewModel
                {
                    InventoryId = inventoryRecord.InventoryId,
                    ProductId = product.ProductId,
                    ZoneId = zone.ZoneId,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    ZoneName = zone.Name,
                    Quantity = inventoryRecord.Quantity,
                    Category = category.Name
                };
            }));

            var vm = new InventoryTableViewModel
            {
                Inventories = inventoryViewModels.ToList()
            };

            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> CreateProductView()
        {
            var loggedUser = await _employeeService.GetByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

            List<Category> categories = (List<Category>)await _categoryService.GetAllAsync(loggedUser.WarehouseId);
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

            List<Zone> zones = (List<Zone>)await _zoneService.GetAllAsync(loggedUser.WarehouseId);
            var availableZones = new List<ZoneViewModel>();

            foreach (var zone in zones)
            {
                var availableZone = new ZoneViewModel
                {
                    ZoneId = zone.ZoneId,
                    Name = zone.Name
                };

                availableZones.Add(availableZone);
            }

            var vm = new CreateProductViewModel
            {
                Categories = availableCategories,
                Zones = availableZones
            };

            return View("CreateProduct", vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct (CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var loggedUser = await _employeeService.GetByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

                List<Category> categories = (List<Category>)await _categoryService.GetAllAsync(loggedUser.WarehouseId);
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

                List<Zone> zones = (List<Zone>)await _zoneService.GetAllAsync(loggedUser.WarehouseId);
                var availableZones = new List<ZoneViewModel>();

                foreach (var zone in zones)
                {
                    var availableZone = new ZoneViewModel
                    {
                        ZoneId = zone.ZoneId,
                        Name = zone.Name
                    };

                    availableZones.Add(availableZone);
                }

                
                model.Categories = availableCategories;
                model.Zones = availableZones;

                return View("CreateProduct", model);
            }

            var newProduct = new Product( 
                model.Name, 
                model.Price, 
                model.SelectedCategory.CategoryId 
            );

            var newInventory = new Inventory(
                model.SelectedZone.ZoneId    
            );

            try
            {
                _inventoryService.AddNewProductTransactionAsync(newProduct, newInventory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", "This product already exists");

                return View("CreateProduct", model);
            }

            return RedirectToAction("Inventory");
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

            var loggedUser = await _employeeService.GetByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

            var newCategory = new Category(model.Name, loggedUser.WarehouseId);

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


        [Authorize]
        public async Task<IActionResult> AddStockView(int id)
        {
            
            var selectedInventory = await _inventoryService.GetByIdAsync(id);

            var selectedProduct = await _productService.GetByIdAsync(selectedInventory.ProductId);

            var selectedProductCategory = await _categoryService.GetByIdAsync(selectedProduct.CategoryId);

            var selectedZone = await _zoneService.GetByIdAsync(selectedInventory.ZoneId);

            var vm = new InventoryViewModel{
                InventoryId = selectedInventory.InventoryId,
                ProductId = selectedProduct.ProductId,
                ProductName = selectedProduct.Name,
                ProductPrice = selectedProduct.Price,
                ZoneName = selectedZone.Name,
                ZoneId = selectedZone.ZoneId,
                Quantity = selectedInventory.Quantity,
                Category = selectedProductCategory.Name
            };

            return View("AddStock",vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddStock(InventoryViewModel model)
        {
            var updatedInventory = new Inventory(
                model.InventoryId,
                model.Quantity,
                DateTime.Now
            );

            _inventoryService.AddStockAsync(updatedInventory);

            return RedirectToAction("Inventory");
        }
    }
}
