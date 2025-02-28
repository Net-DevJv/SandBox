using ApiEmployees.Enums;
using ApiEmployees.Models;

namespace ApiEmployees.Mapping
{
    public static class EmployeesMapper
    {
        public static void CreateEmployeeProperties(this EmployeesModel employee, EmployeesModel newEmployee)
        {
            employee.Name = newEmployee.Name;
            employee.LastName = newEmployee.LastName;
            employee.Department = newEmployee.Department;
            employee.Status = Status.Created;
            employee.Shift = newEmployee.Shift;
            employee.CreationDate = DateTime.Now;
        }

        public static void UpdateEmployeeProperties(this EmployeesModel employee, EmployeesModel editEmployee)
        {
            employee.Name = editEmployee.Name;
            employee.LastName = editEmployee.LastName;
            employee.Department = editEmployee.Department;
            employee.Status = editEmployee.Status;
            employee.Shift = editEmployee.Shift;
            employee.UpdateDate = DateTime.Now;
        }

        public static void ActivateEmployeeProperties(this EmployeesModel employee)
        {
            employee.Status = Status.Active;
            employee.UpdateDate = DateTime.Now;
        }

        public static void DeactivateEmployeeProperties(this EmployeesModel employee)
        {
            employee.Status = Status.Inactive;
            employee.UpdateDate = DateTime.Now;
        }

        public static void DeleteEmployeeProperties(this EmployeesModel employee)
        {
            employee.Status = Status.Deleted;
            employee.UpdateDate = DateTime.Now;
        }
    }
}
