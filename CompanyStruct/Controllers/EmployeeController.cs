using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStruct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        //Get all employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        //Get employee by ID
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int employeeId)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return NotFound($"Employee ID {employeeId} not found");
            }

            return Ok(employee);
        }

        //Add new employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            var (isSuccess, errors) = await _employeeService.AddAsync(employee);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok($"Employee ID {employee.Id} ADDED");
        }

        //Update employee by ID
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, Employee employee)
        {
            var (isSuccess, errors) = await _employeeService.UpdateAsync(employeeId, employee);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Employee ID {employeeId} UPDATED");
        }

        //Delete employee by ID
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeById(int employeeId)
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