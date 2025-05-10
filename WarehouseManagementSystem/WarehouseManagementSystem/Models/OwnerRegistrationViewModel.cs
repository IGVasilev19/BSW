using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Models
{
    public class OwnerRegistrationViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int StreetNumber { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Zip { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string WarehouseName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required"), EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one number, and be at least 8 characters.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "This field is required"), StringLength(22)]
        public string PhoneNumber { get; set; }
    }
}
