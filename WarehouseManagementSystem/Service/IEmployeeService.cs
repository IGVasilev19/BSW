using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IEmployeeService : Service<Employee>
    {
        public Task<bool> RegisterOwnerWithWarehouseAsync(Address address, Warehouse warehouse, Employee employee);

        public Task<bool> CreateAsync(Employee employee);

        public Task<Employee> AuthenticateEmployeeAsync(string email, string password);

        public Task UpdateActivityAsync (string email, bool activity);

        public Task<Employee> GetByEmailAsync (string email);

        public Task<IEnumerable<Employee>> GetAllAsync(int id);
    }
}
