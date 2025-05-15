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
        [Range(1, 2, ErrorMessage = "Please select a valid category")]
        public int SelectedCategory { get; set; }
        [BindNever]
        [ValidateNever]
        public SelectList Categories { get; set; }
    }
}
