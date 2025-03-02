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
            employee.Status = Status.Created;
            employee.CreationDate = DateTime.Now;

            var random = new Random();

            var departments = Enum.GetValues(typeof(DepartmentType));
            employee.Department = (DepartmentType)departments.GetValue(random.Next(departments.Length));

            var shifts = Enum.GetValues(typeof(ShiftType));
            employee.Shift = (ShiftType)shifts.GetValue(random.Next(shifts.Length));
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
    }
}
