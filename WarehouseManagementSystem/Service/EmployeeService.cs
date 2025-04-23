using BLL;
using DAL;

namespace Service
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

        public void UpdateEmployeeRole(int id, Role role) => _repo.UpdateRole(id, role);

        public void DeleteEmployee(int id) => _repo.Delete(id);
    }
}
