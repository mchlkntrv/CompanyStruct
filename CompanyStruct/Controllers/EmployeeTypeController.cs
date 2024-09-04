using CompanyStruct.Services;
using CompanyStruct.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CompanyStruct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTypeController(IEmployeeTypeService employeeTypeService) : ControllerBase
    {
        private readonly IEmployeeTypeService _employeeTypeService = employeeTypeService;

        //Get all employee types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeType>>> GetAllEmployees()
        {
            var employeeTypes = await _employeeTypeService.GetAllAsync();
            return Ok(employeeTypes);
        }

        //Get employee type by ID
        [HttpGet("{employeeTypeId}")]
        public async Task<ActionResult<EmployeeType>> GetEmployeeById(int employeeTypeId)
        {
            var employeeType = await _employeeTypeService.GetByIdAsync(employeeTypeId);

            if (employeeType == null)
            {
                return NotFound($"Employee type {employeeTypeId} not found");
            }

            return Ok(employeeType);
        }

        //Add new employee type
        [HttpPost]
        public async Task<ActionResult<EmployeeType>> AddEmployee(EmployeeType employeeType)
        {
            var (isSuccess, errors) = await _employeeTypeService.AddAsync(employeeType);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok($"Employee type {employeeType.Id} {employeeType.TypeName} ADDED");
        }

        //Update employee type by ID
        [HttpPut("{employeeTypeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeTypeId, EmployeeType employeeType)
        {
            var (isSuccess, errors) = await _employeeTypeService.UpdateAsync(employeeTypeId, employeeType);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Employee type {employeeType.Id} {employeeType.TypeName} UPDATED");
        }

        //Delete employee type by ID
        [HttpDelete("{employeeTypeId}")]
        public async Task<IActionResult> DeleteEmployeeById(int employeeTypeId)
        {
            var (isSuccess, errors) = await _employeeTypeService.DeleteAsync(employeeTypeId);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Employee type {employeeTypeId} DELETED");
        }
    }
}
