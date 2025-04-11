using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Employee> GetAllEmployees() => _repo.GetAll();

        public Employee? GetEmployeeById(int id) => _repo.GetById(id);

        public void CreateEmployee(Employee employee) => _repo.Add(employee);

        public void UpdateEmployee(Employee employee) => _repo.Update(employee);

        public void DeleteEmployee(int id) => _repo.Delete(id);
    }
}
