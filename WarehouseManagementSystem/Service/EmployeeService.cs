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

        public async Task<Employee> AuthenticateEmployeeAsync(string email, string password)
        {
            var employee = await _repo.GetByEmailAsync(email);

            if (PasswordHasher.Verify(password, employee.Password))
            {
                return employee;
            }

            return null;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task UpdateActivityAsync (string email, bool activity) => await _repo.UpdateActivityAsync(email, activity);
    }
}
