using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Models
{
    public class EditEmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int SelectedRole { get; set; }
        [BindNever]
        [ValidateNever]
        public SelectList Roles { get; set; }
    }
}
