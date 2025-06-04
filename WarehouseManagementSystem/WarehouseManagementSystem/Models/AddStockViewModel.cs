using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Models
{
    public class AddStockViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public int Quantity { get; set; }
        public DateTime LasUpdate { get; set; }
        [Required(ErrorMessage = "Please select a zone")]
        public ZoneViewModel SelectedZone { get; set; }
        [BindNever]
        [ValidateNever]
        public List<ZoneViewModel> Zones { get; set; }
        [Required(ErrorMessage = "Please select a product")]
        public ProductViewModel SelectedProduct { get; set; }
        [BindNever]
        [ValidateNever]
        public List<ProductViewModel> Products { get; set; }
    }
}
