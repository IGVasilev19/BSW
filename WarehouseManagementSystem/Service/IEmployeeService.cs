using Entities;
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

        public Task<Employee> AuthenticateEmployeeAsync(string email, string password);
    }
}
