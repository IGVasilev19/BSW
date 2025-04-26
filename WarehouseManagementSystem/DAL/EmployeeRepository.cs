using BLL;

namespace DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbHelper _db;

        public EmployeeRepository(DbHelper db)
        {
            _db = db;
        }

        public void Add(Employee employee)
        {
            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = _db.CreateCommand(@"INSERT INTO Employee (Name, Email, Password, PhoneNumber, Role, EmployeeStatus, WarehouseId) VALUES (@Name, @Email, @Password, @PhoneNumber, @Role, @EmployeeStatus, @WarehouseId)", conn);

            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Password", employee.Password);
            cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Role", (int)employee.Role);
            cmd.Parameters.AddWithValue("@EmployeeStatus", (int)employee.EmployeeStatus);
            cmd.Parameters.AddWithValue("@WarehouseId", employee.WarehouseId);

            cmd.ExecuteNonQuery();
        }

        public void DeleteById(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = _db.CreateCommand(@"DELETE FROM Address WHERE AddressId = @AddressId", conn);

            cmd.Parameters.AddWithValue("@AddressId", id);

            cmd.ExecuteNonQuery();
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
                list.Add(new Employee(
                    (int)reader["EmployeeId"],
                    reader["Name"].ToString(),
                    reader["Email"].ToString(),
                    reader["Password"].ToString(),
                    reader["PhoneNumber"].ToString(),
                    (Role)reader["Role"],
                    (EmployeeStatus)reader["EmployeeStatus"],
                    (int)reader["WarehouseId"]
                ));
            }
            return list;
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole (int id, Role role)
        {
            throw new NotImplementedException();
        }

        public void Update (Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}