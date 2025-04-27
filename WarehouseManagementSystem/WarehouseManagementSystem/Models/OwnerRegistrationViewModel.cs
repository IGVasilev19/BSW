using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Models
{
    public class OwnerRegistrationViewModel
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public int StreetNumber { get; set; }
        [Required]
        public int Zip { get; set; }

        [Required]
        public string WarehouseName { get; set; }

        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one number, and be at least 8 characters.")]
        public string Password { get; set; }
        [Required, StringLength(22)]
        public string PhoneNumber { get; set; }
    }
}
