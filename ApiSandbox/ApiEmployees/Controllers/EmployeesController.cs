using ApiEmployees.Common;
using ApiEmployees.Models;
using Microsoft.AspNetCore.Mvc;
using ApiEmployees.Service.EmployeesService;

namespace ApiEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ServiceResponse<List<EmployeesModel>>>> CreateEmployees(EmployeesModel newEmployee)
        {
            return Ok(await _employeesService.CreateEmployees(newEmployee));
        }

        [HttpGet("Return")]
        public async Task<ActionResult<ServiceResponse<List<EmployeesModel>>>> GetEmployees()
        {
            return Ok(await _employeesService.GetEmployees());
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<ServiceResponse<EmployeesModel>>> GetEmployeesById(int id)
        {
            ServiceResponse<EmployeesModel> serviceResponse = await _employeesService.GetEmployeesById(id);

            return Ok(serviceResponse);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<List<EmployeesModel>>>> UpdateEmployees(EmployeesModel editEmployee)
        {
            ServiceResponse<List<EmployeesModel>> serviceResponse = await _employeesService.UpdateEmployees(editEmployee);

            return Ok(serviceResponse);
        }

        [HttpPut("Activate")]
        public async Task<ActionResult<ServiceResponse<List<EmployeesModel>>>> ActivateEmployees(int id)
        {
            ServiceResponse<List<EmployeesModel>> serviceResponse = await _employeesService.ActivateEmployees(id);

            return Ok(serviceResponse);
        }

        [HttpPut("Deactivate")]
        public async Task<ActionResult<ServiceResponse<List<EmployeesModel>>>> DeactivateEmployees(int id)
        {
            ServiceResponse<List<EmployeesModel>> serviceResponse = await _employeesService.DeactivateEmployees(id);

            return Ok(serviceResponse);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ServiceResponse<List<EmployeesModel>>>> DeleteEmployees(int id)
        {
            ServiceResponse<List<EmployeesModel>> serviceResponse = await _employeesService.DeleteEmployees(id);

            return Ok(serviceResponse);
        }
    }
}
