using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystem.Models
{
    public class EmployeesPageViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; }
        public CreateEmployeeViewModel CreateEmployee { get; set; }
        public bool ShowCreateForm { get; set; }
    }
}
