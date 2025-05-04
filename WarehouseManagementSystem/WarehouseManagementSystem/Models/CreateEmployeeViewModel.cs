using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystem.Models
{
    public class CreateEmployeeViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Role SelectedRole { get; set; }
        public SelectList Roles { get; set; }
    }
}
