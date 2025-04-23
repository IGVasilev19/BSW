using BLL;

namespace DAL
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        void Add(Employee employee);
        void UpdateRole(int id, Role role);
        void Delete(int id);
    }
}