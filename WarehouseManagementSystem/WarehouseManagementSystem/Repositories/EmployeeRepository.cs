using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DBHelper _db;

        public EmployeeRepository(DBHelper db)
        {
            _db = db;
        }

        public void Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            var list = new List<Employee>();
            using var conn = _db.GetConnection();
            conn.Open();
            var cmd = _db.CreateCommand("SELECT * FROM Employees", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Employee
                {
                    Id = (int)reader["EmloyeeId"],
                    Name = reader["Name"].ToString() ?? "",
                    Email = reader["Email"].ToString() ?? "",
                    Role = (int)reader["Role"]
                });
            }
            return list;
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
