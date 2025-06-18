using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Service;
using Microsoft.AspNetCore.Authorization;
using Service.Interfaces;
using Service.Strategies.Pricing;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderProductService _orderProductService;

        public OrdersController(IOrderProductService orderProductService)
        {
            _orderProductService = orderProductService;
        }

        [Authorize]
        public IActionResult Orders()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult CreateOrderView()
        {
            return View("CreateOrder");
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        //{

        //    string strategyKey;

        //    if (DateTime.Today == new DateTime(2025, 7, 1))
        //        strategyKey = PricingStrategyKeys.OnSale;
        //    else
        //        strategyKey = PricingStrategyKeys.Standard;

        //    decimal total = _orderProductService.CalculateTotalPrice(rice, quantity, strategyKey);

        //    return RedirectToAction("Orders");
        //}

        public IActionResult TestPricing()
        {
            decimal unitPrice = 100;
            int quantity = 2;

            string strategyKey;

            if (DateTime.Today == new DateTime(2025, 7, 1))
                strategyKey = PricingStrategyKeys.Standard;
            else
                strategyKey = PricingStrategyKeys.OnSale;
            

            decimal total = _orderProductService.CalculateTotalPrice(unitPrice, quantity, strategyKey);

            return Content($"Calculated total: {total}");
        }

    }
}
