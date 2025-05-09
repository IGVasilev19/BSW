using Entities;
using DAL;
using Exceptions;

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

        public async Task<bool> RegisterOwnerWithWarehouseAsync (Address address, Warehouse warehouse, Employee employee)
        {
            var secureEmployee = new Employee(employee.Name, employee.Email, PasswordHasher.Hash(employee.Password), employee.PhoneNumber);
            try
            {
                await _repo.RegisterWithWarehouseTransactionAsync(address, warehouse, secureEmployee);
                return true;
            }
            catch (QueryFailedException ex)
            {
                return false;
            }
        }

        public async Task<Employee> AuthenticateEmployeeAsync (string email, string password)
        {
            var employee = await _repo.GetByEmailAsync(email);

            if (PasswordHasher.Verify(password, employee.Password))
            {
                return employee;
            }

            return null;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync (int warehouseId) => await _repo.GetAllAsync(warehouseId);

        public async Task<IEnumerable<Employee>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task UpdateActivityAsync (string email, bool activity) => await _repo.UpdateActivityAsync(email, activity);

        public async Task<Employee> GetByIdAsync (int id) => await _repo.GetByIdAsync(id);

        public async Task<Employee> GetByEmailAsync (string email) => await _repo.GetByEmailAsync(email);

        async Task Service<Employee>.CreateAsync(Employee employee) 
        {
            throw  new NotImplementedException();
        }

        public async Task<bool> CreateAsync(Employee employee)
        {
            try
            {
                await _repo.AddAsync(employee);
                return true;
            }
            catch (QueryFailedException ex)
            { 
                return false;
                throw ex;
            }
        }
    }
}
