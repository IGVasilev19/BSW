using System.Diagnostics;
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

        public InventoryController (IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
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
        public IActionResult CreateZone()
        {
            return View();
        }
    }
}
