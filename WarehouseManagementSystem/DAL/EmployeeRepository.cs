using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Exceptions;

namespace DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbHelper _db;
        private readonly IAddressRepository _addressRepository;
        private readonly IWarehouseRepository _warehouseRepository;

        public EmployeeRepository(DbHelper db, IAddressRepository addressRepository, IWarehouseRepository warehouseRepository)
        {
            _db = db;
            _addressRepository = addressRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task AddAsync(Employee employee)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            try
            {
                var cmd = _db.CreateCommand(@"INSERT INTO Employee (Name, Email, Password, PhoneNumber, Role, IsActive, WarehouseId) VALUES (@Name, @Email, @Password, @PhoneNumber, @Role, @IsActive, @WarehouseId)", conn);

                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Password", employee.Password);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@Role", (int)employee.Role);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                cmd.Parameters.AddWithValue("@WarehouseId", employee.WarehouseId);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new QueryFailedException("Employee with this email already exists",ex);
            }
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
            var cmd = _db.CreateCommand("SELECT * FROM Employee", conn);

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

        public async Task<IEnumerable<Employee>> GetAllAsync(int warehouseId)
        {
            var list = new List<Employee>();

            using var conn = _db.GetConnection();
            await conn.OpenAsync();
            var cmd = _db.CreateCommand("SELECT * FROM Employee WHERE WarehouseId = @WarehouseId", conn);

            cmd.Parameters.AddWithValue("@WarehouseId", warehouseId);

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

        public async Task<Employee> GetByEmailAsync(string email)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var cmd = _db.CreateCommand("SELECT * FROM Employee WHERE Email = @Email", conn);
            cmd.Parameters.AddWithValue("@Email", email);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Employee(
                    (int)reader["EmployeeId"],
                    reader["Name"].ToString(),
                    reader["Email"].ToString(),
                    reader["Password"].ToString(),
                    reader["PhoneNumber"].ToString(),
                    (Role)reader["Role"],
                    (bool)reader["IsActive"],
                    (int)reader["WarehouseId"]
                );
            }

            return null;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var cmd = _db.CreateCommand("SELECT * FROM Employee WHERE EmployeeId = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Employee(
                    (int)reader["EmployeeId"],
                    reader["Name"].ToString(),
                    reader["Email"].ToString(),
                    reader["Password"].ToString(),
                    reader["PhoneNumber"].ToString(),
                    (Role)reader["Role"],
                    (bool)reader["IsActive"],
                    (int)reader["WarehouseId"]
                );
            }

            return null;
        }

        public async Task UpdateRoleAsync (int id, Role role)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync (Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateActivityAsync (string email, bool activity)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var cmd = _db.CreateCommand(@"UPDATE Employee SET IsActive = @IsActive WHERE Email = @Email", conn);

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@IsActive", activity);

            cmd.ExecuteNonQuery();
        }

        public async Task RegisterWithWarehouseTransactionAsync(Address address, Warehouse warehouse, Employee employee)
        {
            using var connection = _db.GetConnection();
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                var addressId = await _addressRepository.AddAsync(address, connection, transaction);
                warehouse = new Warehouse(warehouse.Name, addressId);
                var warehouseId = await _warehouseRepository.AddAsync(warehouse, connection, transaction);

                employee = new Employee(employee.Name, employee.Email, employee.Password, employee.PhoneNumber, employee.Role, employee.IsActive, warehouseId);

                await AddAsync(employee, connection, transaction);

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new QueryFailedException("Failed to register owner with warehouse", ex);
            }
        }
    }
}