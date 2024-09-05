using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStruct.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        // GET: api/employees/all
        // Get all employees
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        // GET: api/employees/{employeeId}
        // Get employee by ID
        [HttpGet("{employeeId:int}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int employeeId)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return NotFound($"Employee ID {employeeId} not found");
            }

            return Ok(employee);
        }

        // POST: api/employees/add
        // Add new employee
        [HttpPost("add")]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
        {
            var (isSuccess, errors) = await _employeeService.AddAsync(employee);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok($"Employee ID {employee.Id} ADDED");
        }

        // PUT: api/employees/update/{employeeId}
        // Update employee by ID
        [HttpPut("update/{employeeId:int}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, Employee employee)
        {
            var (isSuccess, errors) = await _employeeService.UpdateAsync(employeeId, employee);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Employee ID {employeeId} UPDATED");
        }

        // DELETE: api/employees/delete/{employeeId}
        // Delete employee by ID
        [HttpDelete("delete/{employeeId:int}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var (isSuccess, errors) = await _employeeService.DeleteAsync(employeeId);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Employee {employeeId} DELETED");
        }
    }
}