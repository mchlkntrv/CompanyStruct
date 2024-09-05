using CompanyStruct.Services;
using CompanyStruct.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CompanyStruct.Controllers
{
    [Route("api/employeetypes")]
    [ApiController]
    public class EmployeeTypeController(IEmployeeTypeService employeeTypeService) : ControllerBase
    {
        private readonly IEmployeeTypeService _employeeTypeService = employeeTypeService;

        // GET: api/employeetypes/all
        // Get all employee types
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<EmployeeType>>> GetAllEmployeeTypes()
        {
            var employeeTypes = await _employeeTypeService.GetAllAsync();
            return Ok(employeeTypes);
        }

        // GET: api/employeetypes/{employeeTypeId}
        // Get employee type by ID
        [HttpGet("{employeeTypeId:int}")]
        public async Task<ActionResult<EmployeeType>> GetEmployeeTypeById(int employeeTypeId)
        {
            var employeeType = await _employeeTypeService.GetByIdAsync(employeeTypeId);

            if (employeeType == null)
            {
                return NotFound("Employee type not found");
            }

            return Ok(employeeType);
        }

        // POST: api/employeetypes/add
        // Add new employee type
        [HttpPost("add")]
        public async Task<ActionResult<EmployeeType>> AddEmployeeType([FromBody] EmployeeType employeeType)
        {
            var (isSuccess, errors) = await _employeeTypeService.AddAsync(employeeType);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok("Employee type added");
        }

        // PUT: api/employeetypes/update/{employeeTypeId}
        // Update employee type by ID
        [HttpPut("update/{employeeTypeId:int}")]
        public async Task<IActionResult> UpdateEmployeeType(int employeeTypeId, EmployeeType employeeType)
        {
            var (isSuccess, errors) = await _employeeTypeService.UpdateAsync(employeeTypeId, employeeType);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok("Employee type updated");
        }

        // DELETE: api/employeetypes/delete/{employeeTypeId}
        // Delete employee type by ID
        [HttpDelete("delete/{employeeTypeId:int}")]
        public async Task<IActionResult> DeleteEmployeeType(int employeeTypeId)
        {
            var (isSuccess, errors) = await _employeeTypeService.DeleteAsync(employeeTypeId);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok("Employee type deleted");
        }
    }
}
