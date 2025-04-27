namespace Entities
{
    public class Employee
    {
        public int EmployeeId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public Role Role { get; private set; }
        public bool IsActive { get; private set; }
        public int WarehouseId { get; private set; }

        public Employee(string name, string email, string password, string phoneNumber)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.PhoneNumber = phoneNumber;
        }

        public Employee(string name, string email, string password, string phoneNumber, Role role, bool isActive, int warehouseId)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.PhoneNumber = phoneNumber;
            this.Role = role;
            this.IsActive = isActive;
            this.WarehouseId = warehouseId;
        }

        public Employee(int employeeId, string name, string email, string password, string phoneNumber, Role role, bool isActive, int warehouseId)
        {
            this.EmployeeId = employeeId;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.PhoneNumber = phoneNumber;
            this.Role = role;
            this.IsActive = isActive;
            this.WarehouseId = warehouseId;
        }
    }
}