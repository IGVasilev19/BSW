using Entities;
using DAL;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IAddressService _addressService;
        private readonly IWarehouseService _warehouseService;
        private readonly DbHelper _db;

        public EmployeeService(IAddressService addressService, IWarehouseService warehouseService, IEmployeeRepository repo, DbHelper db)
        {
            _repo = repo;
            _addressService = addressService;
            _warehouseService = warehouseService;
            _db = db;
        }

        public Task<IEnumerable<Employee>> GetAllAsync() => _repo.GetAllAsync();

        public Task<Employee> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public Task CreateAsync(Employee employee) => _repo.AddAsync(employee);

        public Task UpdateAsync(Employee employee) => _repo.UpdateAsync(employee);

        public Task DeleteByIdAsync(int id) => _repo.DeleteByIdAsync(id);

        public Task UpdateRoleAsync(int id, Role role) => _repo.UpdateRoleAsync(id, role);

        public async Task<bool> RegisterOwnerWithWarehouseAsync(Address address, Warehouse warehouse, Employee employee)
        {
            using var connection = _db.GetConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                var addressId = await _addressService.CreateAsync(address, connection, transaction);
                warehouse = new Warehouse(warehouse.Name, addressId);

                var warehouseId = await _warehouseService.CreateAsync(warehouse, connection, transaction);

                string hashedPassword = PasswordHasher.Hash(employee.Password);

                employee = new Employee(employee.Name, employee.Email, hashedPassword, employee.PhoneNumber, employee.Role, employee.IsActive, warehouseId);

                await _repo.AddAsync(employee, connection, transaction);

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
