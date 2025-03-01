using ApiEmployees.Enums;
using ApiEmployees.Models;
using ApiEmployees.Common;
using ApiEmployees.Mapping;
using ApiEmployees.DataContext;
using ApiEmployees.ManagerResource;
using Microsoft.EntityFrameworkCore;

namespace ApiEmployees.Service.EmployeesService
{
    public class EmployeesService : IEmployeesService
    {
        private readonly WebApiDbContext _context;

        public EmployeesService(WebApiDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<EmployeesModel>>> CreateEmployees(EmployeesModel newEmployee)
        {
            var serviceResponse = new ServiceResponse<List<EmployeesModel>>();

            try
            {
                if (newEmployee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = MessageManager.GetMessage("EX202");

                    return serviceResponse;
                }

                var employee = new EmployeesModel();
                employee.CreateEmployeeProperties(newEmployee);

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Employees.ToList();
                serviceResponse.Message = MessageManager.GetMessage("IS102");
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessageManager.GetMessage("SYS500");
                serviceResponse.Success = false;
            }

            return serviceResponse; ;
        }

        public async Task<ServiceResponse<List<EmployeesModel>>> GetEmployees()
        {
            var serviceResponse = new ServiceResponse<List<EmployeesModel>>();

            try
            {
                var employees = await _context.Employees.ToListAsync();
                serviceResponse.Data = employees;

                if (employees.Any())
                {
                    serviceResponse.Message = MessageManager.GetMessage("IS100");
                }
                else
                {
                    serviceResponse.Message = MessageManager.GetMessage("EX200");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessageManager.GetMessage("SYS500");
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeesModel>> GetEmployeesById(int id)
        {
            var serviceResponse = new ServiceResponse<EmployeesModel>();

            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = MessageManager.GetMessage("EX201");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Data = employee;
                    serviceResponse.Message = MessageManager.GetMessage("IS101");
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = MessageManager.GetMessage("SYS500");
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeesModel>>> UpdateEmployees(EmployeesModel editEmployee)
        {
            var serviceResponse = new ServiceResponse<List<EmployeesModel>>();

            try
            {
                var employee = _context.Employees.FirstOrDefault(x => x.Id == editEmployee.Id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = MessageManager.GetMessage("EX201");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                employee.UpdateEmployeeProperties(editEmployee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Employees.ToList();
                serviceResponse.Message = MessageManager.GetMessage("IS104");
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessageManager.GetMessage("SYS500");
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeesModel>>> ActivateEmployees(int id)
        {
            var serviceResponse = new ServiceResponse<List<EmployeesModel>>();

            try
            {
                var employee = _context.Employees.FirstOrDefault(x => x.Id == id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = MessageManager.GetMessage("EX201");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                if (employee.Status == Status.Active)
                {
                    serviceResponse.Message = MessageManager.GetMessage("EX204");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                employee.ActivateEmployeeProperties();
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Employees.ToList();
                serviceResponse.Message = MessageManager.GetMessage("IS106");
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessageManager.GetMessage("SYS500");
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeesModel>>> DeactivateEmployees(int id)
        {
            var serviceResponse = new ServiceResponse<List<EmployeesModel>>();

            try
            {
                var employee = _context.Employees.FirstOrDefault(x => x.Id == id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = MessageManager.GetMessage("EX201");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                if (employee.Status == Status.Inactive)
                {
                    serviceResponse.Message = MessageManager.GetMessage("EX203");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                employee.DeactivateEmployeeProperties();
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Employees.ToList();
                serviceResponse.Message = MessageManager.GetMessage("IS103");
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessageManager.GetMessage("SYS500");
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeesModel>>> DeleteEmployees(int id)
        {
            var serviceResponse = new ServiceResponse<List<EmployeesModel>>();

            try
            {
                var employee = _context.Employees.FirstOrDefault(x => x.Id == id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = MessageManager.GetMessage("EX201");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                employee.DeleteEmployeeProperties();
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Employees.ToList();
                serviceResponse.Message = MessageManager.GetMessage("IS105");
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessageManager.GetMessage("SYS500");
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
