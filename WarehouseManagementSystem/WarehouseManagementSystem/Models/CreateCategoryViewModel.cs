using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Models
{
    public class CreateCategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
    }
}
