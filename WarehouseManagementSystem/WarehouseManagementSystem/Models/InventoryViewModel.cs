using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Models
{
    public class InventoryViewModel
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int ZoneId { get; set; }
        public string ProductName { get; set; }
        public string ZoneName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }
}