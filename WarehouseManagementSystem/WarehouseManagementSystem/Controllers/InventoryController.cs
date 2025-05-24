using System.Diagnostics;
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
            List<CreateCategoryViewModel> categories = (List<CreateCategoryViewModel>)await _categoryService.GetAllAsync();

            var vm = new CreateProductViewModel
            {
                Categories = new SelectList(categories, "CategoryId", "Name")
            };

            return View("CreateProduct", vm);
        }

        [Authorize]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [Authorize]
        public IActionResult CreateZone()
        {
            return View();
        }
    }
}
