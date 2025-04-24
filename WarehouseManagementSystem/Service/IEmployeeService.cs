using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IEmployeeService : Service<Employee>
    {
        public void UpdateRole(int id, Role role);
    }
}
