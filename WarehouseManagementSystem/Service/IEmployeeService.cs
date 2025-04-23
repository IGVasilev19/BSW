using BLL;

namespace Service
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int Id);
        void CreateEmployee(Employee employee);
        void UpdateEmployeeRole(int id, Role role);
        void DeleteEmployee(int id);
    }
}
