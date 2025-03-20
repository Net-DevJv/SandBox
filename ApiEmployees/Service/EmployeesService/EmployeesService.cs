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
                serviceResponse.Message = MessageManager.GetMessage("IS100");
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
                    serviceResponse.Message = MessageManager.GetMessage("IS101");
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
                    serviceResponse.Message = MessageManager.GetMessage("EX200");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Data = employee;
                    serviceResponse.Message = string.Format(MessageManager.GetMessage("IS102"), employee.Id);
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

        public async Task<ServiceResponse<EmployeesModel>> UpdateEmployees(EmployeesModel editEmployee)
        {
            var serviceResponse = new ServiceResponse<EmployeesModel>();

            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == editEmployee.Id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = MessageManager.GetMessage("EX200");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                employee.UpdateEmployeeProperties(editEmployee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = employee;
                serviceResponse.Message = string.Format(MessageManager.GetMessage("IS103"), employee.Id);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessageManager.GetMessage("SYS500");
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeesModel>> ActivateEmployees(int id)
        {
            var serviceResponse = new ServiceResponse<EmployeesModel>();

            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = MessageManager.GetMessage("EX200");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                if (employee.Status == Status.Active)
                {
                    serviceResponse.Message = string.Format(MessageManager.GetMessage("EX203"), employee.Id);
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                employee.ActivateEmployeeProperties();
                await _context.SaveChangesAsync();

                serviceResponse.Data = employee;
                serviceResponse.Message = string.Format(MessageManager.GetMessage("IS104"), employee.Id);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessageManager.GetMessage("SYS500");
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeesModel>> DeactivateEmployees(int id)
        {
            var serviceResponse = new ServiceResponse<EmployeesModel>();

            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = MessageManager.GetMessage("EX200");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                if (employee.Status == Status.Inactive)
                {
                    serviceResponse.Message = string.Format(MessageManager.GetMessage("EX202"), employee.Id);
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                employee.DeactivateEmployeeProperties();
                await _context.SaveChangesAsync();

                serviceResponse.Data = employee;
                serviceResponse.Message = string.Format(MessageManager.GetMessage("IS105"), employee.Id);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessageManager.GetMessage("SYS500");
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeesModel>> DeleteEmployees(int id)
        {
            var serviceResponse = new ServiceResponse<EmployeesModel>();

            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = MessageManager.GetMessage("EX200");
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = employee;
                serviceResponse.Message = string.Format(MessageManager.GetMessage("IS106"), employee.Id);
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
