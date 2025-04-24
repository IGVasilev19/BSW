using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IEmployeeRepository : Repository<Employee>
    {
        public void UpdateRole(int id, Role role);
    }
}
