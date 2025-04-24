using BLL;
using DAL;

namespace Service
{
    public class EmployeeService : Service<Employee>
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Employee> GetAll() => _repo.GetAll();

        public Employee? GetById(int id) => _repo.GetById(id);

        public void Create(Employee employee) => _repo.Add(employee);

        public void Update(Employee employee) => _repo.Update(employee);

        public void DeleteById(int id) => _repo.DeleteById(id);

        public void UpdateRole(int id, Role role) => _repo.UpdateRole(id, role);
    }
}
