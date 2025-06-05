using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Models
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please select a category")]
        public CreateCategoryViewModel SelectedCategory { get; set; }
        [BindNever]
        [ValidateNever]
        public List<CreateCategoryViewModel> Categories { get; set; }
        [Required(ErrorMessage = "Please select a zone")]
        public ZoneViewModel SelectedZone { get; set; }
        [BindNever]
        [ValidateNever]
        public List<ZoneViewModel> Zones { get; set; }
    }
}
