using Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IEmployeeRepository : Repository<Employee>
    {
        public Task UpdateRoleAsync (int id, Role role);
        public Task AddAsync(Employee employee, SqlConnection conn, SqlTransaction tran);
        public Task<Employee> GetByEmailAsync (string email);
        public Task UpdateActivityAsync(string email, bool activity);
        public Task<IEnumerable<Employee>> GetAllAsync(int id);
        public Task RegisterWithWarehouseTransactionAsync(Address address, Warehouse warehouse, Employee employee);
    }
}
