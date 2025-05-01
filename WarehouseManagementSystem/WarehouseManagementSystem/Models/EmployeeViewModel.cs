namespace WarehouseManagementSystem.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; }
        public int WarehouseId { get; set; }
    }
}