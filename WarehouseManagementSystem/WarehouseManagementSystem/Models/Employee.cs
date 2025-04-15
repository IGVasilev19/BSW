using WarehouseManagementSystem.Models.Enums;

namespace WarehouseManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public Role Role { get; private set; }
        public EmployeeStatus EmployeeStatus { get; private set; }
        public int WarehouseId { get; private set; }

        public Employee(int employeeId, string name, string email, string password,string phoneNumber, Role role, EmployeeStatus employeeStatus, int warehouseId)
        {
            this.EmployeeId = employeeId;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.PhoneNumber = phoneNumber;
            this.Role = role;
            this.EmployeeStatus = employeeStatus;
            this.WarehouseId = warehouseId;
        }
    }
}
