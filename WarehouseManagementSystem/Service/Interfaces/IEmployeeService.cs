using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEmployeeService : Service<Employee>
    {
        public Task RegisterOwnerWithWarehouseAsync(Address address, Warehouse warehouse, Employee employee);

        public Task<Employee> AuthenticateEmployeeAsync(string email, string password);

        public Task UpdateActivityAsync(string email, bool activity);

        public Task<Employee> GetByEmailAsync(string email);

        public Task<IEnumerable<Employee>> GetAllAsync(int employeeId, int warehouseId);

        public Task UpdateRoleAsync(int id, Role role);
    }
}
