using Entities;
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
        Task AddAsync(Employee employee, SqlConnection conn, SqlTransaction tran);
    }
}
