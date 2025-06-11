using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Models
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int WarehouseId { get; set; }
    }
}
