using ApiEmployees.Common;
using ApiEmployees.Models;

namespace ApiEmployees.Service.EmployeesService
{
    public interface IEmployeesService
    {
        Task<ServiceResponse<List<EmployeesModel>>> CreateEmployees(EmployeesModel newEmployee);

        Task<ServiceResponse<List<EmployeesModel>>> GetEmployees();

        Task<ServiceResponse<EmployeesModel>> GetEmployeesById(int id);

        Task<ServiceResponse<List<EmployeesModel>>> UpdateEmployees(EmployeesModel editEmployee);

        Task<ServiceResponse<List<EmployeesModel>>> ActivateEmployees(int id);

        Task<ServiceResponse<List<EmployeesModel>>> DeactivateEmployees(int id);

        Task<ServiceResponse<List<EmployeesModel>>> DeleteEmployees(int id);
    }
}
