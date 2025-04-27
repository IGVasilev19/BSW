using Entities;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbHelper _db;

        public EmployeeRepository(DbHelper db)
        {
            _db = db;
        }

        public async Task AddAsync(Employee employee)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var cmd = _db.CreateCommand(@"INSERT INTO Employee (Name, Email, Password, PhoneNumber, Role, IsActive, WarehouseId) VALUES (@Name, @Email, @Password, @PhoneNumber, @Role, @IsActive, @WarehouseId)", conn);

            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Password", employee.Password);
            cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Role", (int)employee.Role);
            cmd.Parameters.AddWithValue("@IsActive", (bool)employee.IsActive);
            cmd.Parameters.AddWithValue("@WarehouseId", employee.WarehouseId);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task AddAsync(Employee employee, SqlConnection connection, SqlTransaction transaction)
        {

            var cmd = _db.CreateCommand(@"INSERT INTO Employee (Name, Email, Password, PhoneNumber, Role, IsActive, WarehouseId) VALUES (@Name, @Email, @Password, @PhoneNumber, @Role, @IsActive, @WarehouseId)", connection, transaction);

            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Password", employee.Password);
            cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Role", 0);
            cmd.Parameters.AddWithValue("@IsActive", 0);
            cmd.Parameters.AddWithValue("@WarehouseId", employee.WarehouseId);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var cmd = _db.CreateCommand(@"DELETE FROM Address WHERE AddressId = @AddressId", conn);

            cmd.Parameters.AddWithValue("@AddressId", id);

            cmd.ExecuteNonQuery();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var list = new List<Employee>();

            using var conn = _db.GetConnection();
            await conn.OpenAsync();
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
                    (bool)reader["IsActive"],
                    (int)reader["WarehouseId"]
                ));
            }
            return list;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRoleAsync (int id, Role role)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync (Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}