using Domain;
using Exceptions;
using System.Data;
using Service.Interfaces;
using Service.Utility;
using DAL.Interfaces;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IAddressService _addressService;
        private readonly IWarehouseService _warehouseService;

        public EmployeeService(IAddressService addressService, IWarehouseService warehouseService, IEmployeeRepository repo)
        {
            _repo = repo;
            _addressService = addressService;
            _warehouseService = warehouseService;
        }

        public async Task RegisterOwnerWithWarehouseAsync (Address address, Warehouse warehouse, Employee employee)
        {
            var secureEmployee = new Employee(employee.Name, employee.Email, PasswordHasher.Hash(employee.Password), employee.PhoneNumber);
            try
            {
                await _repo.RegisterWithWarehouseTransactionAsync(address, warehouse, secureEmployee);
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> AuthenticateEmployeeAsync (string email, string password)
        {
            var employee = await _repo.GetByEmailAsync(email);

            if (employee == null)
            {
                return null;
            }
            else if (PasswordHasher.Verify(password, employee.Password))
            {
                return employee;
            }

            return null;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync (int employeeId, int warehouseId) => await _repo.GetAllAsync(employeeId, warehouseId);

        public async Task<IEnumerable<Employee>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task UpdateActivityAsync (string email, bool activity) => await _repo.UpdateActivityAsync(email, activity);

        public async Task<Employee> GetByIdAsync (int id) => await _repo.GetByIdAsync(id);

        public async Task<Employee> GetByEmailAsync (string email) => await _repo.GetByEmailAsync(email);

        public async Task CreateAsync(Employee employee)
        {
            try
            {
                var secureEmployee = new Employee(employee.Name,employee.Email,PasswordHasher.Hash(employee.Password),employee.PhoneNumber, employee.Role, employee.IsActive, employee.WarehouseId);

                await _repo.AddAsync(secureEmployee);
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }
        }

        public async Task UpdateRoleAsync(int id, Role role) => await _repo.UpdateRoleAsync(id, role);

        public async Task DeleteByIdAsync(int id) => await _repo.DeleteByIdAsync(id);
    }
}
